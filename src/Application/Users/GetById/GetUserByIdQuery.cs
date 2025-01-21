using Application.Abstractions.Messaging;
using Application.DTOs.User;

namespace Application.Users.GetById;

public sealed record GetUserByIdQuery(string UserId) : IQuery<UserDto>;
