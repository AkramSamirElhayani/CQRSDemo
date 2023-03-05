namespace CQRSDemo.Models
{
    public class Order
    {
        public Order()
        {
            Id  = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
