using Application.BusinessRules;
using Application.Repositories.Abstractions;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Topics.Queries.GetComments;

public class GetCommentsQuery:IRequest<GetCommentsQueryResponse>
{
    public int Id { get; set; }
    
    public class GetCommentsQueryHandler: IRequestHandler<GetCommentsQuery, GetCommentsQueryResponse>
    {
        private readonly TopicBusinessRules _topicBusinessRules;
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;

        public GetCommentsQueryHandler(ITopicRepository topicRepository, IMapper mapper, TopicBusinessRules topicBusinessRules)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
            _topicBusinessRules = topicBusinessRules;
        }

        public async Task<GetCommentsQueryResponse> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var topic = await _topicRepository.GetWhere(
                t => t.Id == request.Id,
                query => query
                    .Include(t => t.Comments)
                    .ThenInclude(c => c.User)
            ).FirstOrDefaultAsync();

            await _topicBusinessRules.UserShouldsBeExists(topic);
            
            var response = new GetCommentsQueryResponse
            {
                Comments = _mapper.Map<List<CommentDto>>(topic.Comments.ToList())
            };

            return response;
        }
    }
}