namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string Description { get; set; }
    public virtual Topic Topic { get; set; }
    public virtual User User { get; set; }
}