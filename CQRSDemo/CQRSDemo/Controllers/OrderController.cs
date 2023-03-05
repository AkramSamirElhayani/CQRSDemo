using CQRSDemo.MediatR.Commands;
using CQRSDemo.MediatR.Queries;
using CQRSDemo.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSDemo.Controllers
{
    [Route("Orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> GetById(Guid id)
        {
            var order =await _mediator.Send(new GetOrderByIdQuery { Id = id });
            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(orders);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Found)
            {
                return NotFound();
            }

            if (!result.Updated)
            {
                return BadRequest("Failed to update order");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result.Found)
            {
                return NotFound();
            }

            if (!result.Deleted)
            {
                return BadRequest("Failed to delete order");
            }

            return Ok(result);
        }
    }
}
