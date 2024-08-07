namespace Domain.Entities;

public class Topic : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }

    public Topic()
    {
    }

    public Topic(int id): base(id)
    {
    }
    public Topic(int id, string title, string description) : base(id)
    {
        Title = title;
        Description = description;
    }
    
}