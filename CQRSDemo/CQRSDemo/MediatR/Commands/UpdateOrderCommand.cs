using CQRSDemo.Models;
using MediatR;

namespace CQRSDemo.MediatR.Commands
{
    public class UpdateOrderCommand :IRequest<UpdateOrderCommandResult>
    {
         
        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

    }
    public class UpdateOrderCommandResult
    {
        public bool Found { get; }
        public bool Updated { get; }

        public UpdateOrderCommandResult(bool found, bool updated)
        {
            Found = found;
            Updated = updated;
        }

        public static UpdateOrderCommandResult UpdatedSucceeded { get => new UpdateOrderCommandResult(true, true); }
        public static UpdateOrderCommandResult NotFound { get => new UpdateOrderCommandResult(false, false); }
        public static UpdateOrderCommandResult UpdateFaild { get => new UpdateOrderCommandResult(true, false); }
    }

}
