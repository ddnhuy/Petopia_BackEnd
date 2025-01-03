﻿namespace Domain.Users;
public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public string UserId { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public ApplicationUser User { get; set; }
}
