using Application.Abstractions.Messaging;

namespace Application.Media.Delete;
public sealed record DeleteMediaCommand(string publicId) : ICommand;
