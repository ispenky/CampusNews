using System.Threading.Tasks;
namespace CampusNews.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}