namespace CQRSDemo.Models
{
    public class OrderItem
    {
        public OrderItem() {

            Id = Guid.NewGuid();

        }
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
