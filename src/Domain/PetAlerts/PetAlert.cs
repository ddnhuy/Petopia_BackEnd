using Domain.Pets;
using SharedKernel;

namespace Domain.PetAlerts;
public sealed class PetAlert : Entity
{
    public Guid PetId { get; set; }
    public string PhoneNumber { get; set; }
    public string Note { get; set; }
    public DateTime LastSeen { get; set; }
    public string Address { get; set; }

    public Pet Pet { get; set; }
}
