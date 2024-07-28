using FluentValidation;

namespace Application.Features.Commands.Auth.Register;

public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty();
        RuleFor(c => c.FirstName)
            .NotEmpty();
        RuleFor(c => c.LastName)
            .NotEmpty();
        RuleFor(c => c.Password)
            .NotEmpty();
    }
}