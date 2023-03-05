using CQRSDemo.Models;
using MediatR;

namespace CQRSDemo.MediatR.Commands
{
    public class CreateOrderCommand :IRequest<Guid>
    {
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public List<OrderItem>?   OrderItems { get; set; }
    }
}
