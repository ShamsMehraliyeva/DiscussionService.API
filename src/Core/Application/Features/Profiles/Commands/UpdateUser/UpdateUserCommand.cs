using System.Security.Claims;
using Application.Repositories.Abstractions;
using Application.Utilities.JWT;
using MediatR;

namespace Application.Features.Profiles.Commands.UpdateUser;

public class UpdateUserCommand: IRequest<UpdateUserCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(ITokenHelper tokenHelper, IUserRepository userRepository)
        {
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            int userId = _tokenHelper.GetUserIdFromToken();

            var getUser = await _userRepository.GetAsync(x=>x.Id == userId);
            getUser.FirstName = request.FirstName;
            getUser.LastName = request.LastName;
            
            await _userRepository.UpdateAsync(getUser);
            return new UpdateUserCommandResponse();
        }
    }
}