using FluentValidation;

namespace Application.Features.Commands.Auth.Login;

public class LoginCommandValidator:AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("'Email' should not be empty");
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("'Password' should not be empty");
    }
}