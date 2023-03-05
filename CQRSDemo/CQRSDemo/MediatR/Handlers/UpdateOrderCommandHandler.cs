using AutoMapper;
using CQRSDemo.DataAccess;
using CQRSDemo.MediatR.Commands;
using CQRSDemo.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.MediatR.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand , UpdateOrderCommandResult>
    {
        private readonly IMediator _mediator;
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public UpdateOrderCommandHandler(IMediator mediator, ApplicationDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
           .Include(o => o.OrderItems)
           .SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
            {
                return UpdateOrderCommandResult.NotFound;
            }

            order.CustomerName = request.CustomerName;
            order.Address = request.Address;

            // Remove deleted order items
             
            _context.OrderItems.RemoveRange(order.OrderItems.Where(oi =>  oi.OrderId == request.Id ).ToList()); 
            await _context.SaveChangesAsync(cancellationToken);
            // Update or add order items
            foreach (var itemDto in request.OrderItems)
            {
               
                    var existingItem = order.OrderItems.SingleOrDefault(oi => oi.Id == itemDto.Id);
                    if (existingItem != null)
                    {
                        _mapper.Map(itemDto, existingItem);
                    }
                    else
                    {
                        itemDto.OrderId = request.Id;
                        order.OrderItems.Add(itemDto);
                    }
              
            }

            int result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0 ? UpdateOrderCommandResult.UpdatedSucceeded : UpdateOrderCommandResult.UpdateFaild;

        }
    }
}
