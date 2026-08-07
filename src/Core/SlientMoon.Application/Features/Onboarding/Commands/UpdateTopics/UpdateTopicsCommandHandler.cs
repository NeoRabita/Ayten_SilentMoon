using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Repositories;
using SlientMoon.Application.Interfaces.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SlientMoon.Domain.Entities;

namespace SlientMoon.Application.Features.Onboarding.Commands.UpdateTopics;

public sealed class UpdateTopicsCommandHandler
    : ICommandHandler<UpdateTopicsCommand>
{
    private readonly IUserService _userService;
    private readonly ITopicRepository _topicRepository;
    private readonly IUserTopicRepository _userTopicRepository;
    private readonly IUow _uow;

    public UpdateTopicsCommandHandler(
     IUserService userService,
     ITopicRepository topicRepository,
     IUserTopicRepository userTopicRepository,
     IUow uow)
    {
        _userService = userService;
        _topicRepository = topicRepository;
        _userTopicRepository = userTopicRepository;
        _uow = uow;
    }
    public async Task<Result> Handle(
        UpdateTopicsCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrentUserAsync();

        var topics = await _topicRepository.GetAllTopicsAsync();

        var selectedTopics = topics
            .Where(x => command.TopicIds.Contains(x.Id))
            .ToList();

        if (selectedTopics.Count != command.TopicIds.Distinct().Count())
        {
            return Result.Failure(
                Error.NotFound(
                    "Topic.NotFound",
                    "Seçilmiş mövzulardan biri tapılmadı."));
        }

        var existingUserTopics =
            await _userTopicRepository.GetUserTopicsAsync(user.Id);

        _userTopicRepository.DeleteRange(existingUserTopics);

        var newUserTopics = selectedTopics
            .Select(topic => new UserTopic
            {
                UserId = user.Id,
                TopicId = topic.Id
            })
            .ToList();

        await _userTopicRepository.AddRangeAsync(
            newUserTopics,
            cancellationToken);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}