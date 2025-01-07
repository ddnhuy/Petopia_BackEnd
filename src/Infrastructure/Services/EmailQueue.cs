using System.Collections.Concurrent;
using Application.Abstractions.Email;

namespace Infrastructure.Services;

internal sealed class EmailQueue : IEmailQueue
{
    private readonly ConcurrentQueue<EmailMessage> _queue = new();

    public void Enqueue(EmailMessage message)
    {
        _queue.Enqueue(message);
    }

    public bool TryDequeue(out EmailMessage? message)
    {
        return _queue.TryDequeue(out message);
    }
}
