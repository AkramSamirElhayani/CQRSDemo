using AutoMapper;
using CQRSDemo.MediatR.Commands;
using CQRSDemo.Models;

namespace CQRSDemo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<OrderItem, OrderItem>();
        }
    }
}
