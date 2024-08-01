using Application.Repositories.Abstractions;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Topics.Commands.CreateTopic;

public class CreateTopicCommand: IRequest<CreateTopicCommandResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    
    public class CreateTopicCommandHandler: IRequestHandler<CreateTopicCommand, CreateTopicCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;

        public CreateTopicCommandHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<CreateTopicCommandResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            Topic addTopic = _mapper.Map<Topic>(request);
            await _topicRepository.AddAsync(addTopic);
            return new CreateTopicCommandResponse();
        }
    }
}