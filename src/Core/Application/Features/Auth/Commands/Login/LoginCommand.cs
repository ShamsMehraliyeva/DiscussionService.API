using Application.BusinessRules;
using Application.Repositories.Abstractions;
using Application.Services;
using Application.Utilities.JWT;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Auth.Login;

public class LoginCommand: IRequest<LoginCommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        
        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetAsync(x => x.Email == request.Email);
            
            await _authBusinessRules.UserShouldsBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user, request.Password);
            
            AccessTokenModel createdAccessTokenModel = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user);
            RefreshToken addedRefreshToken = await _authService.UpdateRefreshTokenTransaction(createdRefreshToken);
            
            LoginCommandResponse response = new()
            {
                RefreshToken = _mapper.Map<RefreshTokenModel>(addedRefreshToken),
                AccessToken = createdAccessTokenModel
            };
            return response;
        }
    }
}