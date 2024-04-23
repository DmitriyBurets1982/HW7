using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
    }
}
