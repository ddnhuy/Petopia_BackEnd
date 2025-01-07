using Application.Abstractions.Email;
using Application.Abstractions.Services;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Workers;

internal class EmailWorker(
    IEmailQueue emailQueue,
    IEmailSender emailSender) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (emailQueue.TryDequeue(out EmailMessage? message) && message is not null)
            {
                await emailSender.SendEmailAsync(message);
            }

            await Task.Delay(100, stoppingToken);
        }
    }
}
