using Application.Abstractions.Messaging;
using Application.DTOs;

namespace Application.Statics.GetPetTypes;
public sealed record GetPetTypesQuery() : IQuery<List<StaticTypeDto>>;
