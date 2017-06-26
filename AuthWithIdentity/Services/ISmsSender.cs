using System.Threading.Tasks;

namespace PaderbornUniversity.SILab.Hip.Auth.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
