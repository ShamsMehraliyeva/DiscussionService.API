using Application.Repositories.Abstractions;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Topics.Queries.GetTopicList;

public class GetTopicListQuery: IRequest<GetTopicListQueryResponse>
{
    public class GetTopicListQueryHanler: IRequestHandler<GetTopicListQuery, GetTopicListQueryResponse>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicListQueryHanler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetTopicListQueryResponse> Handle(GetTopicListQuery request, CancellationToken cancellationToken)
        {
            GetTopicListQueryResponse response = new();
            List<Topic> getTopics = _topicRepository.GetAll().ToList();
            foreach (var topic in getTopics)
            {
                response.Topics.Add(new TopicDto(){Id = topic.Id, Title = topic.Title, Description = topic.Description});
            }

            return response;
        }
    }
}