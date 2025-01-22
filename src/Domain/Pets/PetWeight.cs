using SharedKernel;

namespace Domain.Pets;
public sealed class PetWeight : Entity
{
    public Guid PetId { get; private set; }
    public double Value { get; set; }

    public PetWeight(Guid petId, double value)
    {
        PetId = petId;
        Value = value;
    }
}
