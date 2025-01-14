using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string FullName() => $"{FirstName} {LastName}";
}
