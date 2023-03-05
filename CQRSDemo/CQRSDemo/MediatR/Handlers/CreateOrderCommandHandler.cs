using CQRSDemo.DataAccess;
using CQRSDemo.MediatR.Commands;
using CQRSDemo.Models;
using MediatR;

namespace CQRSDemo.MediatR.Handlers
{
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand,Guid>
    {
        private readonly IMediator _mediator;
        private ApplicationDbContext _context;

        public CreateOrderCommandHandler(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderCommand command ,CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerName = command.CustomerName,
                Address = command.Address,
                OrderItems = command.OrderItems
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
