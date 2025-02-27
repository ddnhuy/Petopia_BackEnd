using Domain.Users;
using SharedKernel;

namespace Domain.Reactions;
public sealed class Reaction : Entity
{
    public ReactionTargetType TargetType { get; set; }
    public string UserId { get; private set; }
    public Guid TargetId { get; private set; }

    public ApplicationUser User { get; set; }

    public Reaction(ReactionTargetType targetType, string userId, Guid targetId)
    {
        TargetType = targetType;
        UserId = userId;
        TargetId = targetId;
    }
}
