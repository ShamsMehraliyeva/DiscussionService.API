using FluentValidation;

namespace Application.Features.Commands.Auth.Login;

public class LoginCommandValidator:AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty();
        RuleFor(c => c.Password)
            .NotEmpty();
    }
}