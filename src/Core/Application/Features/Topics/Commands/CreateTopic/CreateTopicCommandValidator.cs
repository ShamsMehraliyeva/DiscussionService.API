using FluentValidation;

namespace Application.Features.Topics.Commands.CreateTopic;

public class CreateTopicCommandValidator:AbstractValidator<CreateTopicCommand>
{
    public CreateTopicCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("'Title' should not be empty");
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("'Description' should not be empty");
    }
}