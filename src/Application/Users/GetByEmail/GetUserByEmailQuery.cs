using Application.Abstractions.Messaging;
using Application.DTOs.User;

namespace Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserDto>;
