using Application.Constants.Messages;
using Application.CrossCuttingConcers.Exceptions;
using Domain.Entities;

namespace Application.BusinessRules;

public class TopicBusinessRules
{
    public async Task UserShouldsBeExists(Topic? topic)
    {
        if (topic == null) throw new BusinessException(TopicMessages.TopicIdDontExists);
    }
}