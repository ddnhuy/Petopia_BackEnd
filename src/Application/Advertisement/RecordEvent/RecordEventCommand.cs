using Application.Abstractions.Messaging;
using Domain.Advertisement;

namespace Application.Advertisement.RecordEvent;
public sealed record RecordEventCommand(Guid AdId, AdEventType EventType) : ICommand;
