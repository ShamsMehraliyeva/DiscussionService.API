using Application.Repositories.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Features.Topics.Commands.CreateTopic;

public class CreateTopicCommand: IRequest<CreateTopicCommandResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public class CreateTopicCommandHandler: IRequestHandler<CreateTopicCommand, CreateTopicCommandResponse>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateTopicCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<CreateTopicCommandResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            Topic addTopic = new Topic(request.Title, request.Description);
            await _topicRepository.AddAsync(addTopic);
            return new CreateTopicCommandResponse();
        }
    }
}