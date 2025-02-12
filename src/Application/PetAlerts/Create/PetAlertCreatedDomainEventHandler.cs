using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using Application.DTOs.Pet;
using Domain.PetAlerts;
using Domain.Pets;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedKernel;

namespace Application.PetAlerts.Create;
internal sealed class PetAlertCreatedDomainEventHandler(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : INotificationHandler<PetAlertCreatedDomainEvent>
{
    public async Task Handle(PetAlertCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        HttpClient client = httpClientFactory.CreateClient("OneSignal");

        var jsonObject = new OneSignalRequest
        {
            AppId = configuration["OneSignal:AppId"]!,
            Headings = new Dictionary<string, string>
            {
                { "en", AppStrings.NotificationTitle_PetAlert }
            },
            Contents = new Dictionary<string, string>
            {
                { "en", AppStrings.NotificationContent_PetAlert(notification.PetAlert.Pet.Name, notification.PetAlert.LastSeen, notification.PetAlert.PhoneNumber, notification.PetAlert.Address, notification.PetAlert.Pet.Gender, EnumHelper.GetDisplayName(notification.PetAlert.Pet.Type)) }
            },
            IncludedSegments = new List<string> { "All" },
            Url = configuration["OneSignal:AppUrl"]! + "PetAlert?" + $"PetAlertId={notification.PetAlert.Id}",
            TargetChannel = "push",
            AndroidGroup = "pet_alerts",
            BigPicture = notification.PetAlert.Pet.ImageUrl,
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

        await client.PostAsync("notifications?c=push", content, cancellationToken);
    }
}
