using Domain.Dtos;

namespace Application.Features.Topics.Queries.GetComments;

public class GetCommentsQueryResponse
{
    public List<CommentDto> Comments { get; set; }

    public GetCommentsQueryResponse()
    {
        Comments = new List<CommentDto>();
    }
}