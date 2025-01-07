namespace Application.Abstractions.Email;

public interface IEmailQueue
{
    void Enqueue(EmailMessage message);
    bool TryDequeue(out EmailMessage? message);
}
