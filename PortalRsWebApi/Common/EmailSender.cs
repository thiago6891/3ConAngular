using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace PortalRSApi.Common
{
    public class EmailSender
    {
        //private IConfigurationRoot _config { get; set; }
        private MyConfiguration _config { get; set; }

        public EmailSender(MyConfiguration cfg)
        {
            _config = cfg;
        }

        public EmailSender(IOptions<MyConfiguration> opt)
        {
            _config = opt.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.EmailFromName, _config.EmailFrom));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // accept all SSL certificates
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                var smtp = _config.Host;

                client.Connect(smtp, 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(
                    _config.UserName,
                    _config.Password
                 );

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}