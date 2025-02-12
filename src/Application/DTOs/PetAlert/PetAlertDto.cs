using Application.DTOs.Pet;

namespace Application.DTOs.PetAlert;
public sealed class PetAlertDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Note { get; set; }
    public DateTime LastSeen { get; set; }
    public string Address { get; set; }
    public DateTime UpdatedAt { get; set; }

    public PetDto Pet { get; set; }
}
