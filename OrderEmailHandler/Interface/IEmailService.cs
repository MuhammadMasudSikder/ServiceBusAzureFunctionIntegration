using System.Threading.Tasks;

namespace OrderEmailHandler.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendEmailAsync(string toEmail, string subject, string plainText, string htmlContent);
    }
}
