using Application.Repositories.Abstractions;
using Application.Utilities.JWT;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Topics.Commands.AddComment;

public class AddCommentCommand : IRequest<AddCommentCommandResponse>
{
    public int Id { get; set; }
    public string? Content { get; set; }

    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, AddCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly ITopicRepository _topicRepository;
        private readonly ICommentRepository _commentRepository;

        public AddCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, ITokenHelper tokenHelper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }

        public async Task<AddCommentCommandResponse> Handle(AddCommentCommand request,
            CancellationToken cancellationToken)
        {
            int userId = _tokenHelper.GetUserIdFromToken();

            Comment createdComment = new Comment
            {
                Content = request.Content,
                UserId = userId,
                TopicId = request.Id
            };
            await _commentRepository.AddAsync(createdComment);

            return new AddCommentCommandResponse();
        }
    }
}