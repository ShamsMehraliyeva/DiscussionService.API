using Application.Extensions.Security;
using Application.Utilities.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Utilities.JWT;

internal class JwtHelper : ITokenHelper
{
    public IConfiguration _configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessTokenModel CreateToken(User user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);
        return new AccessTokenModel
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public RefreshToken CreateRefreshToken(User user)
    {
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            Token = RandomRefreshToken(),
            ExpireDate = DateTime.UtcNow.AddDays(7),
            CreateDate = DateTime.UtcNow
        };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                                                   SigningCredentials signingCredentials)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddName($"{user.FirstName} {user.LastName}");
        return claims;
    }
    private string RandomRefreshToken()
    {
        byte[] numberByte = new Byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
    
    //Get
    public string GetToken()
    {
        var context = _httpContextAccessor.HttpContext;
        var authorizationHeader = context?.Request.Headers["Authorization"].FirstOrDefault();
        var token = authorizationHeader?.Replace("Bearer ", "").Trim();

        return token;
    }
    public string GetUserClaim(string token, string claimName)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
            return claimValue;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public int GetUserIdFromToken()
    {
        var token = this.GetToken();
        var userIdClaim = this.GetUserClaim(token, ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new InvalidOperationException("User ID claim is not valid.");
        }

        return userId;
    }
}
