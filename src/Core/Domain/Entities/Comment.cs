namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public int  TopicId { get; set; }
    public virtual Topic Topic { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}