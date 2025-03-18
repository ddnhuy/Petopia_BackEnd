using Application.Abstractions.Messaging;

namespace Application.Advertisement.Delete;
public sealed record DeleteAdCommand(Guid Id) : ICommand;
