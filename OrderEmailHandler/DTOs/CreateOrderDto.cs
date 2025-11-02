using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEmailHandler.DTOs
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

        public List<CreateOrderItemDto> Items { get; set; }
    }

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
