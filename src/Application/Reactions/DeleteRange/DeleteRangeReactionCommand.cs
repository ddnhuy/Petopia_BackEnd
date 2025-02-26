using Application.Abstractions.Messaging;
using Application.DTOs.Reaction;
using Domain.Reactions;

namespace Application.Reactions.DeleteRange;
public sealed record DeleteRangeReactionCommand(List<ReactionDto> TargetList) : ICommand;
