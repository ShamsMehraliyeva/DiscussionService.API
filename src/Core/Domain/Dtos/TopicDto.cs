namespace Domain.Dtos;

public class TopicDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CommentDto> Comments { get; set; }
}