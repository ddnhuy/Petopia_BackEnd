using Application.Abstractions.Messaging;
using Application.DTOs;

namespace Application.Statics.GetNotificationTypes;
public sealed record GetNotificationTypesQuery() : IQuery<List<StaticTypeDto>>;
