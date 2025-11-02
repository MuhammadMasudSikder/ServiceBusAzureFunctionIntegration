namespace ServiceBusTestApi.Models
{
    public class CreateOrderDto
    {
        public int Id { get; set; }

        // Contact Info
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? AltPhone { get; set; }
        public string? Notes { get; set; }

        // Summary
        public decimal TotalAmount { get; set; }
        public string? OrderDate { get; set; }

        public List<CreateOrderItemDto>? Items { get; set; }
    }
}
