namespace ServiceBusTestApi.Models
{
    public class CreateOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? VendorId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }
}
