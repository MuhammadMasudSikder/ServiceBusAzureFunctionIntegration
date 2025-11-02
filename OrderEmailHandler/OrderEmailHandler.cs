using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using OrderEmailHandler.DTOs;
using OrderEmailHandler.Interface;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderEmailHandler
{
    public class OrderEmailHandler
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        public OrderEmailHandler(ILoggerFactory loggerFactory, IEmailService emailService)
        {
            _logger = loggerFactory.CreateLogger<OrderEmailHandler>();
            _emailService = emailService;
        }

        [Function("OrderEmailHandler")]
        public async Task Run([ServiceBusTrigger("orderplaced",  Connection = "ServiceBusConnection")] string message)
        {
            try
            {
                if(string.IsNullOrEmpty(message))
                {   
                    _logger.LogWarning("Received empty message from Service Bus.");
                    return;
                }
                var order = JsonSerializer.Deserialize<CreateOrderDto>(message);
                _logger.LogInformation($"Processing OrderPlaced event for Order ID: {order?.Id}");

                if (order == null)
                {
                    _logger.LogWarning("Invalid order payload received.");
                    return;
                }

                await _emailService.SendEmailAsync(
                    order.Email,
                     $"Order Confirmation #{order.Id}",
                    $"Hi {order.Name},\n\nYour order totaling ৳ {order.TotalAmount} has been received.\n\nThank you for shopping with us!\n\nWearistyle Team"
                );

                //var smtp = new SmtpClient("smtp.gmail.com")
                //{
                //    Port = 587,
                //    Credentials = new NetworkCredential("your_email@gmail.com", "your_app_password"),
                //    EnableSsl = true,
                //};

                //var mail = new MailMessage
                //{
                //    From = new MailAddress("your_email@gmail.com", "Wearistyle"),
                //    Subject = $"Order Confirmation #{order.Id}",
                //    Body = $"Hi {order.Name},\n\nYour order totaling {order.TotalAmount:C} has been received.\n\nThank you for shopping with us!\n\nWearistyle Team"
                //};

                //mail.To.Add(order.Email);
                //await smtp.SendMailAsync(mail);


                _logger.LogInformation($"Confirmation email sent to {order.Email}");
            }
            catch(Exception ex)
            {
                _logger.LogError($" {ex.Message}");
            }
        }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
