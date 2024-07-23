using Application.Contracts.Auth;
using Application.Extensions.Security;
using Application.Helpers.Security.Encryption;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Helpers.Security.TokenHelper;

internal class JwtHelper : ITokenHelper
{
    public IConfiguration _configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        throw new NotImplementedException();
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
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
}
