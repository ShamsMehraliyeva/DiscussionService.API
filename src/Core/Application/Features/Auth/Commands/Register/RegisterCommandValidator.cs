using FluentValidation;

namespace Application.Features.Commands.Auth.Register;

public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("'Email' should not be empty");
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("'FirstName' should not be empty");
        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("'LastName' should not be empty");
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("'Password' should not be empty");
    }
}