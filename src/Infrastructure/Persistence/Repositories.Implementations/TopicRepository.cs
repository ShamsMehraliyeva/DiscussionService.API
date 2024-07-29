using Application.Repositories.Abstractions;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class TopicRepository : Repository<Topic>, ITopicRepository
{
    public TopicRepository(BaseDbContext context) : base(context)
    {
    }
}