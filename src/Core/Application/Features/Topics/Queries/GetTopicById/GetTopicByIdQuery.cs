using Application.BusinessRules;
using Application.Repositories.Abstractions;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Topics.Queries.GetTopicById;

public class GetTopicByIdQuery: IRequest<GetTopicByIdQueryResponse>
{
    public int Id { get; set; }
    public class GetTopicByIdQueryHandler: IRequestHandler<GetTopicByIdQuery, GetTopicByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly TopicBusinessRules _topicBusinessRules;
        private readonly ITopicRepository _topicRepository;

        public GetTopicByIdQueryHandler(ITopicRepository topicRepository, TopicBusinessRules topicBusinessRules, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _topicBusinessRules = topicBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetTopicByIdQueryResponse> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            Topic getTopic = await _topicRepository.GetAsync(request.Id);
            await _topicBusinessRules.UserShouldsBeExists(getTopic);
            
            GetTopicByIdQueryResponse response = _mapper.Map<GetTopicByIdQueryResponse>(getTopic);

            return response;
        }
    }
}