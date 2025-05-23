﻿using Domain.Pets;
using Domain.Posts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Uri? ImageUrl { get; set; }
    public string? ImagePublicId { get; set; }

    public ICollection<Pet> Pets { get; set; } = [];
    public ICollection<Post> Posts { get; set; } = [];

    public string FullName() => $"{FirstName} {LastName}";

    public ApplicationUser() { }

    public ApplicationUser(string firstName, string lastName, string email, Uri? imageUrl, string? imagePublicId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ImageUrl = imageUrl;
        ImagePublicId = imagePublicId;
    }
}
