using Application.Abstractions.Messaging;
using Application.DTOs;

namespace Application.Statics.GetReactionTargetTypes;
public sealed record GetReactionTargetTypesQuery() : IQuery<List<StaticTypeDto>>;
