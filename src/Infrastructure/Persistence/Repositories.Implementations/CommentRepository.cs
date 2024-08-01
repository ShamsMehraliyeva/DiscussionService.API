using Application.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementations;

public class CommentRepository: Repository<Comment>, ICommentRepository
{
    public CommentRepository(BaseDbContext context) : base(context)
    {
    }
}