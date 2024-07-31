﻿using Application.BusinessRules;
using Application.Repositories.Abstractions;
using Application.Services;
using Application.Utilities.Hashing;
using Application.Utilities.JWT;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Auth.Register;

public class RegisterCommand : IRequest<RegisteredCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
        }

        public async Task<RegisteredCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        { 
           await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.Email);
           byte[] passwordHash, passwordSalt;
           HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            User newUser = new()
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Status = true
            };

            User createdUser = await _userRepository.AddAsync(newUser);
            AccessTokenModel createdAccessTokenModel = await _authService.CreateAccessToken(createdUser);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            
            RegisteredCommandResponse response = new()
            {
                RefreshToken = _mapper.Map<RefreshTokenModel>(addedRefreshToken),
                AccessToken = createdAccessTokenModel
            };
            return response;
        }
    }
}
