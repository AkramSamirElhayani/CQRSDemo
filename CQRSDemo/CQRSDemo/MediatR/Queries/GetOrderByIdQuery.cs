using CQRSDemo.Models;
using MediatR;

namespace CQRSDemo.MediatR.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }
    }
}
