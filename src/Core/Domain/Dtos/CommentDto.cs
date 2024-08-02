namespace Domain.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public UserDto User { get; set; }
}