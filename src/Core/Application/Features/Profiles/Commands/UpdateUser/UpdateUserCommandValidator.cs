using Application.Features.Commands.Auth.Register;
using FluentValidation;

namespace Application.Features.Profiles.Commands.UpdateUser;

public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("'FirstName' should not be empty");
        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("'LastName' should not be empty");

    }
}