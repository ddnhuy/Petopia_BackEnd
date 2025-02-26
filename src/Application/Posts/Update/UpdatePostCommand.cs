using Application.Abstractions.Messaging;

namespace Application.Posts.Update;
public sealed record UpdatePostCommand(Guid Id, string Caption, string HashTag, Uri ImageUrl, string ImagePublicId) : ICommand;
