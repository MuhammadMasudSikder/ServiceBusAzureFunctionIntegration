
using Swashbuckle.AspNetCore.Filters;

namespace ServiceBusTestApi.Models
{
    public class CreateOrderDtoExample : IExamplesProvider<CreateOrderDto>
    {
        public CreateOrderDto GetExamples()
        {
            return new CreateOrderDto
            {
                Id = 25,
                Name = "Masud",
                Email = "muhammadmasudsikder@gmail.com",
                Phone = "2134124",
                ShippingAddress = "sfdfsdf",
                ShippingCity = "Dhaka",
                AltPhone = "",
                Notes = "",
                TotalAmount = 1545,
                OrderDate = "11/02/2025",
                Items = null

            };
        }
    }
}
