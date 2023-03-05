using CQRSDemo.DataAccess;
using CQRSDemo.MediatR.Queries;
using CQRSDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.MediatR.Handlers
{
    public class GetOrdersQueryHandler :IRequestHandler<GetOrdersQuery,List<Order>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrdersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            return await _context.Orders.Include(x=>x.OrderItems).ToListAsync(cancellationToken);
        }
    }
}
