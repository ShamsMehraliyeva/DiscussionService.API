using Application.Repositories.Abstractions;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Topics.Queries.GetTopicList;

public class GetTopicListQuery: IRequest<GetTopicListQueryResponse>
{
    public class GetTopicListQueryHanler: IRequestHandler<GetTopicListQuery, GetTopicListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;

        public GetTopicListQueryHanler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<GetTopicListQueryResponse> Handle(GetTopicListQuery request, CancellationToken cancellationToken)
        {
            GetTopicListQueryResponse response = new();
            List<Topic> getTopicsList= _topicRepository.GetAll().ToList();
            response.Topics = _mapper.Map<List<TopicDto>>(getTopicsList);
            return response;
        }
    }
}