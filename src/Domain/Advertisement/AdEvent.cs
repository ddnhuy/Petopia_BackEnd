using SharedKernel;

namespace Domain.Advertisement;
public class AdEvent : Entity
{
    public Guid AdId { get; set; }
    public Ad Ad { get; set; }

    public AdEventType EventType { get; set; }

    public AdEvent(Guid adId, AdEventType eventType)
    {
        AdId = adId;
        EventType = eventType;
    }
}
