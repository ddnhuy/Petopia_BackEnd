using Application.Abstractions.Messaging;

namespace Application.Posts.Create;
public sealed record CreatePostCommand(string Caption, string HashTag, Uri ImageUrl, string ImagePublicId) : ICommand;
