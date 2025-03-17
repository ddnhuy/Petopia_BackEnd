using SharedKernel;

namespace Domain.Advertisement;
public sealed class Ad : Entity
{
    public string Title { get; set; }
    public Uri TargetUrl { get; set; }
    public double TotalCost { get; set; }
    public int TotalImpressions { get; set; }
    public int TotalClicks { get; set; }

    public bool IsActive { get; set; } = true;

    public Uri ImageUrl { get; set; }
    public string ImagePublicId { get; set; }

    public Ad(string title, Uri targetUrl, double totalCost, Uri imageUrl, string imagePublicId)
    {
        Title = title;
        TargetUrl = targetUrl;
        TotalCost = totalCost;
        ImageUrl = imageUrl;
        ImagePublicId = imagePublicId;
    }
}
