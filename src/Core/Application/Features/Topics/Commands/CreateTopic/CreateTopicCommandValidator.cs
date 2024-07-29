using FluentValidation;

namespace Application.Features.Topics.Commands.CreateTopic;

public class CreateTopicCommandValidator:AbstractValidator<CreateTopicCommand>
{
    public CreateTopicCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);
        RuleFor(c => c.Description)
            .NotEmpty()
            .MinimumLength(5);
    }
}