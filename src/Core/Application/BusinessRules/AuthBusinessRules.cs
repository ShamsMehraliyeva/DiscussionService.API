using Application.Constants.Messages;
using Application.CrossCuttingConcers.Exceptions;
using Application.Repositories.Abstractions;
using Application.Utilities.Hashing;
using Domain.Entities;

namespace Application.BusinessRules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
    {
        User? user = await _userRepository.GetAsync(u=>u.Email==email);
        if (user != null) throw new BusinessException(AuthMessages.EmailAlreadyExists);
    }
    public async Task UserShouldsBeExists(User? user)
    {
        if (user == null) throw new AuthorizationException(AuthMessages.UserDontExists);
    }
    public async Task UserPasswordShouldBeMatch(User? user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new AuthorizationException(AuthMessages.PasswordDontMatch);
    }
}
