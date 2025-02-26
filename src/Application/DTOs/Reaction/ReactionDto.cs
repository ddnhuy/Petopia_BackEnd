using Domain.Reactions;

namespace Application.DTOs.Reaction;
public sealed class ReactionDto
{
    public Guid TargetId { get; set; }
    public ReactionTargetType TargetType { get; set; }
}
