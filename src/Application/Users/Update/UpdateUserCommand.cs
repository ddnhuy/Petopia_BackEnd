using Application.Abstractions.Messaging;
using Application.DTOs.User;

namespace Application.Users.Update;
public sealed record UpdateUserCommand(UserDto UserDTO) : ICommand;
