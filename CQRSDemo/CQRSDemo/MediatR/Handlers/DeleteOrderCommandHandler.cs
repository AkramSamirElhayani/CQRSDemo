using CQRSDemo.DataAccess;
using CQRSDemo.MediatR.Commands;
using CQRSDemo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.MediatR.Handlers
{
    public class DeleteOrderCommandHandler :IRequestHandler<DeleteOrderCommand, DeleteOrderCommandResult>
    {

        private readonly ApplicationDbContext _context;
        public DeleteOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
            {
                return DeleteOrderCommandResult.NotFound;
            }

           _context.Orders.Remove(order);
             int result = await _context.SaveChangesAsync(cancellationToken);
            return result>0? DeleteOrderCommandResult.DeleteSucceeded : DeleteOrderCommandResult.DeleteFaild;

        }
    }
}
