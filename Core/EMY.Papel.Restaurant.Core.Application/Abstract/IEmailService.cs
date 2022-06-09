using System.Net.Mail;

namespace EMY.Papel.Restaurant.Core.Application.Abstract
{
    public interface IEmailService
    {
        Task<(bool IsSuccess, string Message)> SendEmail(string email, string subject, string message, MailPriority mailPriority);
    }
}
