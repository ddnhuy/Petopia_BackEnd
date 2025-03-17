namespace Application.DTOs.Advertisement;
public sealed record AdStatisticsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Uri TargetUrl { get; set; }

    public double TotalCost { get; set; }
    public int TotalImpressions { get; set; }
    public int TotalClicks { get; set; }
    public double CostPerImpression => TotalImpressions > 0 ? TotalCost / TotalImpressions : 0;
    public double CostPerClick => TotalClicks > 0 ? TotalCost / TotalClicks : 0;
    public double ClickThroughRate => TotalImpressions > 0 ? (double)TotalClicks / TotalImpressions * 100 : 0;

    public Uri ImageUrl { get; set; }
    public string ImagePublicId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
