using CQRSDemo.DataAccess;
using CQRSDemo.MediatR.Queries;
using CQRSDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.MediatR.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {

        private readonly ApplicationDbContext _context;

        public GetOrderByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {

            /// Will Need TO handel some Null Exeption 
            /// But For now lets Fouces On the Mediator Pattern 

#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.

        }
    }
}
