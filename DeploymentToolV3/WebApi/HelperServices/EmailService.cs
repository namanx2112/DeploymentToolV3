namespace WebApi.HelperServices
{
    using System.Net;
    using System.Net.Mail;
    using Microsoft.Extensions.Configuration;

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _host;
        private readonly int _port;


        public EmailService(IConfiguration config)
        {
            _config = config;
            var emailConfig = _config.GetSection("EmailConfiguration");
            _fromEmail = emailConfig["FromEmail"];
            _fromPassword = emailConfig["FromPassword"];
            _host = emailConfig["Host"];
            _port = Convert.ToInt32(emailConfig["Port"]);

        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = _host;
                smtpClient.Port = _port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.From = new MailAddress(_fromEmail);
                    emailMessage.To.Add(toEmail);
                    emailMessage.Subject = subject;
                    emailMessage.Body = body;

                    await smtpClient.SendMailAsync(emailMessage);
                }
            }
        }
    }

}
