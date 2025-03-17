using Application.Abstractions.Messaging;

namespace Application.Advertisement.Create;
public sealed record CreateAdCommand(string Title, Uri TargetUrl, double TotalCost, Uri ImageUrl, string ImagePublicId) : ICommand;
