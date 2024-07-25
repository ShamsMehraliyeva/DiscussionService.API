using Application.CrossCuttingConcers.Exceptions;
using Application.Repositories.Abstractions;
using Domain.Entities.Auth;

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
        if (user != null) throw new BusinessException("Mail already exists");

    }
}
