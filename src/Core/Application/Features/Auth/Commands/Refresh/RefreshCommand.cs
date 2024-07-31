using Application.BusinessRules;
using Application.Repositories.Abstractions;
using Application.Services;
using Application.Utilities.JWT;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Refresh;

public class RefreshCommand: IRequest<RefreshCommandResponse>
{
    public string RefreshToken { get; set; }
    
    public class RefreshCommandHanler: IRequestHandler<RefreshCommand, RefreshCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RefreshCommandHanler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService, IMapper mapper)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<RefreshCommandResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            
            await _authBusinessRules.TokenShouldsValid(user);
            
            AccessTokenModel createdAccessTokenModel = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user);
            RefreshToken addedRefreshToken = await _authService.UpdateRefreshTokenTransaction(createdRefreshToken);
            
            
            RefreshCommandResponse response = new()
            {
                RefreshToken = _mapper.Map<RefreshTokenModel>(addedRefreshToken),
                AccessToken = createdAccessTokenModel
            };
            return response;
        }
    }
}