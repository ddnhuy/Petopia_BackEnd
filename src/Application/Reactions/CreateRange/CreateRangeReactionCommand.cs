using Application.Abstractions.Messaging;
using Application.DTOs.Reaction;
using Domain.Reactions;

namespace Application.Reactions.CreateRange;
public sealed record CreateRangeReactionCommand(List<ReactionDto> TargetList) : ICommand;
