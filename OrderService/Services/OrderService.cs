using System.Text;
using System.Text.Json;
using AutoMapper;
using Contracts.Billing;
using Contracts.Notifications.Orders;
using MassTransit;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService(HttpClient httpClient, IMapper mapper, IBus bus, IConfiguration configuration, ILogger<OrderService> logger) : IOrderService
    {
        public async Task CreateOrder(Order order)
        {
            var accountOperation = mapper.Map<AccountOperation>(order);
            var httpContent = new StringContent(JsonSerializer.Serialize(accountOperation), Encoding.UTF8, "application/json");
            var url = $"{configuration["BillingService"]}/billingservice/billing/decrease";
            logger.LogInformation("BillingService url: '{Url}'", url);

            using var response = await httpClient.PutAsync(url, httpContent);
            var isCreated = await response.Content.ReadFromJsonAsync<bool>();

            var notification = new Notification
            {
                AccountId = order.AccountId,
                Price = order.Price,
                Result = isCreated ? NotificationResult.Accepted : NotificationResult.Rejected,
            };
            
            await bus.Publish(notification);
        }
    }
}
