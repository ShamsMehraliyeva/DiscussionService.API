using Domain.Dtos;

namespace Application.Features.Topics.Queries.GetTopicById;

public class GetTopicByIdQueryResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}