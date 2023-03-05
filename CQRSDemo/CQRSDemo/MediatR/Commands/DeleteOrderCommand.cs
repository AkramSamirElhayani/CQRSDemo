using MediatR;

namespace CQRSDemo.MediatR.Commands
{
    public class DeleteOrderCommand :IRequest<DeleteOrderCommandResult>
    {
        public Guid Id { get; set; }
    }
    public class DeleteOrderCommandResult
    {
        public bool Found { get; }
        public bool Deleted { get; }

        public DeleteOrderCommandResult(bool found, bool deleted)
        {
            Found = found;
            Deleted = deleted;
        }

        public static DeleteOrderCommandResult DeleteSucceeded { get => new DeleteOrderCommandResult(true, true); }
        public static DeleteOrderCommandResult NotFound { get => new DeleteOrderCommandResult(false, false); }
        public static DeleteOrderCommandResult DeleteFaild { get => new DeleteOrderCommandResult(true, false); }
    }
}
