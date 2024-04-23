using System.Text.Json;
using Contracts.Notifications.Orders;
using MassTransit;
using NotificationService.Services;

namespace NotificationService.Consumers
{
    public class NotificationConsumer(INotificationService notificationService, ILogger<NotificationConsumer> logger) : IConsumer<Notification>
    {
        public Task Consume(ConsumeContext<Notification> context)
        {
            logger.LogInformation(
                $"Message is consumed by '{nameof(NotificationConsumer)}': '{JsonSerializer.Serialize(context.Message)}");

            notificationService.Save(context.Message.AccountId, context.Message);

            return Task.CompletedTask;
        }
    }
}
