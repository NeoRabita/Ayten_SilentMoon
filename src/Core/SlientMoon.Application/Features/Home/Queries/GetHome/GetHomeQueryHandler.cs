using Application.Abstractions.Messaging;
using SlientMoon.Application.Interfaces.Authentication;
using SlientMoon.Application.Interfaces.Services;
using SlientMoon.Domain.Entities;
using SlientMoon.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SlientMoon.Application.Features.Home.Queries.GetHome
{
    public class GetHomeQueryHandler : IQueryHandler<GetHomeQuery, HomeResponse>
    {
        private readonly IUow _uow;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorageService _fileStorage;

        public GetHomeQueryHandler(
            IUow uow,
            ICurrentUser currentUser,
            IFileStorageService fileStorage)
        {
            _uow = uow;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }

        public async Task<Result<HomeResponse>> Handle(
            GetHomeQuery query,
            CancellationToken ct)
        {
            var contentRepo = _uow.GetRepository<Content>();
            var contentTopicRepo = _uow.GetRepository<ContentTopic>();
            var userTopicRepo = _uow.GetRepository<UserTopic>();
            var contentNarratorRepo = _uow.GetRepository<ContentNarrator>();

            var contents = (await contentRepo.GetAllAsync(ct)).ToList();
            var contentTopics = (await contentTopicRepo.GetAllAsync(ct)).ToList();
            var userTopics = (await userTopicRepo.GetAllAsync(ct)).ToList();
            var contentNarrators = (await contentNarratorRepo.GetAllAsync(ct)).ToList();

            // İstifadəçinin seçdiyi topic-lər
            var userTopicIds = userTopics
                .Where(x => x.UserId == _currentUser.UserId)
                .Select(x => x.TopicId)
                .ToHashSet();

            // Həmin topic-lərə uyğun content-lər
            var recommendedContentIds = contentTopics
                .Where(x => userTopicIds.Contains(x.TopicId))
                .Select(x => x.ContentId)
                .ToHashSet();

            var recommendedContents = contents
                .Where(x => recommendedContentIds.Contains(x.Id))
                .OrderBy(x => x.SortOrder);

            // Daily Thought
            var dailyThoughtContent = contents
                .Where(x => x.IsDailyThought)
                .OrderBy(x => x.SortOrder)
                .FirstOrDefault();

            // Featured Sleep
            var featuredSleepContents = contents
                .Where(x =>
                    x.Category == ContentCategory.Sleep &&
                    x.IsFeatured)
                .OrderBy(x => x.SortOrder);

            // Popular Meditations
            var popularMeditationContents = contents
                .Where(x =>
                    x.Category == ContentCategory.Meditation &&
                    x.IsPopular)
                .OrderBy(x => x.SortOrder);

            var recommendedTask = ToItemListAsync(
                recommendedContents,
                contentTopics,
                contentNarrators,
                ct);

            var featuredSleepTask = ToItemListAsync(
                featuredSleepContents,
                contentTopics,
                contentNarrators,
                ct);

            var popularMeditationsTask = ToItemListAsync(
                popularMeditationContents,
                contentTopics,
                contentNarrators,
                ct);

            var dailyThoughtTask = dailyThoughtContent == null
                ? Task.FromResult<HomeItemResponse>(null)
                : ToItemAsync(
                    dailyThoughtContent,
                    contentTopics,
                    contentNarrators,
                    ct);

            await Task.WhenAll(
                recommendedTask,
                featuredSleepTask,
                popularMeditationsTask,
                dailyThoughtTask);

            var response = new HomeResponse
            {
                Recommended = new HomeSectionResponse
                {
                    Title = "Recommended For You",
                    Items = recommendedTask.Result
                },

                DailyThought = dailyThoughtTask.Result,

                FeaturedSleep = new HomeSectionResponse
                {
                    Title = "Sleep",
                    Items = featuredSleepTask.Result
                },

                PopularMeditations = new HomeSectionResponse
                {
                    Title = "Popular Meditations",
                    Items = popularMeditationsTask.Result
                }
            };

            return Result.Success(response);
        }

        private async Task<List<HomeItemResponse>> ToItemListAsync(
            IEnumerable<Content> contents,
            List<ContentTopic> contentTopics,
            List<ContentNarrator> contentNarrators,
            CancellationToken ct)
        {
            var responses = await Task.WhenAll(
                contents.Select(content =>
                    ToItemAsync(
                        content,
                        contentTopics,
                        contentNarrators,
                        ct)));

            return responses.ToList();
        }

        private async Task<HomeItemResponse> ToItemAsync(
            Content content,
            List<ContentTopic> contentTopics,
            List<ContentNarrator> contentNarrators,
            CancellationToken ct)
        {
            var categoryId = contentTopics
                .FirstOrDefault(x => x.ContentId == content.Id)
                ?.TopicId;

            var narrators = contentNarrators
                .Where(x => x.ContentId == content.Id)
                .Select(x => x.Gender.ToString().ToLowerInvariant())
                .ToList();

            var imageUrl = await _fileStorage.GetPresignedUrlAsync(
                MinioBucket.Media,
                content.ThumbnailUrl,
                ct);

            return new HomeItemResponse
            {
                Id = content.Id.ToString(),
                Title = content.Title,
                Subtitle = content.Subtitle,
                Type = content.Category.ToString().ToLowerInvariant(),
                CategoryId = categoryId?.ToString(),
                ImageUrl = imageUrl,
                DurationSec = content.DurationSeconds,
                IsFeatured = content.IsFeatured,
                Narrators = narrators
            };
        }
    }
}