using System.Security.Claims;
using Application.Repositories.Abstractions;
using Application.Utilities.JWT;
using MediatR;

namespace Application.Features.Profiles.Queries.GetCurrentUser;

public class GetCurrentUserQuery:IRequest<GetCurrentUserQueryResponse>
{
    public class GetCurrentUserQueryHandler: IRequestHandler<GetCurrentUserQuery, GetCurrentUserQueryResponse>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;

        public GetCurrentUserQueryHandler(ITokenHelper tokenHelper, IUserRepository userRepository)
        {
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
        }

        public async Task<GetCurrentUserQueryResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenHelper.GetUserClaim(_tokenHelper.GetToken(), ClaimTypes.NameIdentifier);

            var user = await _userRepository.GetAsync(x => x.Id == Convert.ToInt32(userId));

            GetCurrentUserQueryResponse response = new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return response;
        }
    }
}