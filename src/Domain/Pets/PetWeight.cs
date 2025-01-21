using SharedKernel;

namespace Domain.Pets;
public sealed class PetWeight : Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PetId { get; private set; }
    public double Value { get; set; }
    public DateTime Date { get; private set; } = DateTime.UtcNow;

    public PetWeight(Guid petId, double value)
    {
        PetId = petId;
        Value = value;
    }
}
