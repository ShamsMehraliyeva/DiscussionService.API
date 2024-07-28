using System.Security.Claims;
using Application.Repositories.Abstractions;
using Application.Utilities.JWT;
using MediatR;

namespace Application.Features.Profiles.Commands.UpdateUser;

public class UpdateUserCommand: IRequest<UpdateUserCommandResponse>
{
    public string FirstNam { get; set; }
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
            int userId = Convert.ToInt32(_tokenHelper.GetUserClaim(_tokenHelper.GetToken(), ClaimTypes.NameIdentifier));

            var getUser = await _userRepository.GetAsync(x=>x.Id == userId);
            getUser.FirstName = request.FirstNam;
            getUser.LastName = request.LastName;
            
            await _userRepository.UpdateAsync(getUser);
            return new UpdateUserCommandResponse();
        }
    }
}