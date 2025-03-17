using Application.Abstractions.Messaging;
using Application.DTOs;

namespace Application.Statics.GetAdEventTypes;
public sealed record GetAdEventTypesQuery : IQuery<List<StaticTypeDto>>;
