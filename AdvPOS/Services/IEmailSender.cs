using AdvPOS.Models.EmailConfigViewModel;
using System.Threading.Tasks;

namespace AdvPOS.Services
{
    public interface IEmailSender
    {
        Task<Task> SendEmailAsync(string email, string subject, string message);
        Task<Task> SendEmailByGmailAsync(SendEmailViewModel vm);
    }
}
