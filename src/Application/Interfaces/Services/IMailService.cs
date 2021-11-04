using VoteApp.Application.Requests.Mail;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}