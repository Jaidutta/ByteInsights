using ByteInsights.Data;
using ByteInsights.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;


namespace ByteInsights.Services
{
    public class EmailService : IBlogEmailSender
    {
        private readonly MailSettings _mailSettings;


        //https://medium.com/@M-B-A-R-K/understanding-the-ioptions-t-in-asp-net-core-5ecb25aebbd2
        public EmailService(ApplicationDbContext context, IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        //This is the content of the SendContactEmailAsync() method
        //The functional portion of this method is the same as SendEmailAsync(), but the body is pre-set

        public async Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.Subject = subject;

            var builder = new BodyBuilder(); // assembles the body of the email
            builder.HtmlBody = $"<b>{name}</b> has sent you an email and can be reached at: <b>{emailFrom}</b><br/><br/>{htmlMessage}";

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;


            // var builder = new BodyBuilder(); // assembles the body of the email
            // builder.HtmlBody = htmlMessage;
            // The next line of code is equivalent to the above 2 lines
            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage,

            };
           
            email.Body = builder.ToMessageBody(); // turning an instance of BodyBuilder to  message body

           

            using var smtp = new SmtpClient();


            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            
        }
    }
}
