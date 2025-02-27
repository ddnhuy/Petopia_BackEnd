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

namespace Application.Comments.Create;
internal sealed class CommentCreatedDomainEventHandler(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    IApplicationDbContext context) : INotificationHandler<CommentCreatedDomainEvent>
{
    public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string notiTitle = AppStrings.NotificationTitle;
        string notiContent = AppStrings.NotificationContent("Bài đăng của bạn có bình luận mới.");

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

        Post post = await context.Posts.FirstAsync(p => p.Id == notification.Comment.PostId, cancellationToken);
        Notification newNotification = new(post.UserId, notiTitle, notiContent, post.Id, NotificationType.NewComment);

        await context.Notifications.AddAsync(newNotification, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
