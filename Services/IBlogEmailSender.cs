using Microsoft.AspNetCore.Identity.UI.Services;

namespace ByteInsights.Services
{
    public interface IBlogEmailSender: IEmailSender
    {
        Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage);
    }
}
