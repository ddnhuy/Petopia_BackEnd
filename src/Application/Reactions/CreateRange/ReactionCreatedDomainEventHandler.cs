using System.Text;
using Application.Abstractions.Data;
using Domain.Comments;
using Domain.Notifications;
using Domain.Posts;
using Domain.Reactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedKernel;

namespace Application.Reactions.CreateRange;
internal sealed class ReactionCreatedDomainEventHandler(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    IApplicationDbContext context) : INotificationHandler<ReactionCreatedDomainEvent>
{
    public async Task Handle(ReactionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string notiTitle = AppStrings.NotificationTitle;
        string notiContent = AppStrings.NotificationContent("Bài đăng của bạn có lượt bày tỏ cảm xúc mới.");

        HttpClient client = httpClientFactory.CreateClient("OneSignal");

        var jsonObject = new OneSignalRequest
        {
            AppId = configuration["OneSignal:AppId"]!,
            Headings = new Dictionary<string, string>
            {
                { "en", notiTitle }
            },
            Contents = new Dictionary<string, string>
            {
                { "en", notiContent }
            },
            IncludedSegments = new List<string> { "All" },
            Url = configuration["OneSignal:AppUrl"]! + "Notifications",
            TargetChannel = "push",
            AndroidGroup = "notifications",
            LargeIcon = configuration["OneSignal:AppIcon"]!,
            SmallIcon = configuration["OneSignal:AppIcon"]!,
            ChromeWebImage = configuration["OneSignal:AppIcon"]!
        };

        string json = JsonConvert.SerializeObject(jsonObject, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await client.PostAsync("notifications?c=push", content, cancellationToken).ConfigureAwait(false);

        Notification newNotification;
        if (notification.Reaction.TargetType == ReactionTargetType.Post)
        {

            Post post = await context.Posts.FirstAsync(p => p.Id == notification.Reaction.TargetId, cancellationToken);
            newNotification = new(post.UserId, notiTitle, notiContent, post.Id, NotificationType.NewReaction);
        }
        else
        {
            Comment comment = await context.Comments.FirstAsync(c => c.Id == notification.Reaction.TargetId, cancellationToken);
            newNotification = new(comment.UserId, notiTitle, notiContent, comment.Id, NotificationType.NewReaction);
        }

        await context.Notifications.AddAsync(newNotification, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
