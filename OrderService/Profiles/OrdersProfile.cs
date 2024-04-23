using AutoMapper;
using Contracts.Billing;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            // Source -> Target
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, AccountOperation>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Price));
        }
    }
}
