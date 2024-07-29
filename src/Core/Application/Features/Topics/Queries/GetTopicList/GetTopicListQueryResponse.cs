using Domain.Dtos;

namespace Application.Features.Topics.Queries.GetTopicList;

public class GetTopicListQueryResponse
{
    public List<TopicDto> Topics { get; set; }

    public GetTopicListQueryResponse()
    {
        Topics = new List<TopicDto>();
    }
}