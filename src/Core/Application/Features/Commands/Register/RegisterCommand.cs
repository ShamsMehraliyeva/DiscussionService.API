using Domain.Dtos.Auth;
using MediatR;

namespace Application.Features.Commands.Register;

public class RegisterCommand : IRequest<RegisteredCommandResponse>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredCommandResponse>
    {
        public Task<RegisteredCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            RegisteredCommandResponse response = new();
            return Task.FromResult(response);
        }
    }
}
