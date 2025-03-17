using Application.Abstractions.Messaging;
using Application.DTOs.Advertisement;

namespace Application.Advertisement.Get;
public sealed record GetAdsQuery(bool? IsActiveOnly = false) : IQuery<IEnumerable<AdDto>>;
