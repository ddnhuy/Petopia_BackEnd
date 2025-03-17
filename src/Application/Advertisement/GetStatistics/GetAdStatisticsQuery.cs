using Application.Abstractions.Messaging;
using Application.DTOs.Advertisement;

namespace Application.Advertisement.GetStatistics;
public sealed record GetAdStatisticsQuery(bool? IsActiveOnly = false) : IQuery<IEnumerable<AdStatisticsDto>>;
