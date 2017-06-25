using System.Threading.Tasks;

namespace PaderbornUniversity.SILab.Hip.Auth.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
