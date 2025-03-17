using Application.Abstractions.Messaging;

namespace Application.Advertisement.Update;
public sealed record UpdateAdCommand(Guid Id, string Title, Uri TargetUrl, double TotalCost, bool IsActive, Uri ImageUrl, string ImagePublicId) : ICommand;
