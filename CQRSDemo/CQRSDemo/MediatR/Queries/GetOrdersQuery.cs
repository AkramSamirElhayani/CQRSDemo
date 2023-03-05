using CQRSDemo.Models;
using MediatR;

namespace CQRSDemo.MediatR.Queries
{
    public class GetOrdersQuery : IRequest<List<Order>>
    {
    }
}
