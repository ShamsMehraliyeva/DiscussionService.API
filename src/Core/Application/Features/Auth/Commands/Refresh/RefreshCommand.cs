using Application.BusinessRules;
using Application.Repositories.Abstractions;
using Application.Services;
using Application.Utilities.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Refresh;

public class RefreshCommand: IRequest<RefreshCommandResponse>
{
    public string RefreshToken { get; set; }
    
    public class RefreshCommandHanler: IRequestHandler<RefreshCommand, RefreshCommandResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RefreshCommandHanler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<RefreshCommandResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            await _authBusinessRules.TokenShouldsBeExists(user);

            var currentRefreshToken = user.RefreshTokens.OrderDescending().LastOrDefault();
            AccessTokenModel createdAccessTokenModel = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user);
            RefreshToken addedRefreshToken = await _authService.UpdateRefreshToken(currentRefreshToken, createdRefreshToken);
            
            RefreshCommandResponse response = new()
            {
                RefreshToken = new()
                {
                    Expiration = addedRefreshToken.ExpireDate,
                    Token = addedRefreshToken.Token
                },
                AccessToken = createdAccessTokenModel
            };
            return response;
        }
    }
}