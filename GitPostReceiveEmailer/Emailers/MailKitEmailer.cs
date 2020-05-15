using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace GitPostReceiveEmailer
{
    /// <summary>
    /// Sends an email using https://github.com/jstedfast/MailKit
    /// </summary>
    public class MailKitEmailer : IEmailer
    {
        /// <summary>
        /// Sends an email via MailKit (see https://github.com/jstedfast/MailKit)
        /// </summary>
        /// <param name="config">The configuration object.</param>
        /// <param name="htmlBody">The body of the email in HTML format.</param>
        /// <param name="textBody">The body of the email in text format (optional).</param>
        public void SendEmail(Configuration config, string htmlBody, string textBody)
        {
            if (config != null)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(config.From.Name, config.From.Address));

                foreach (var em in config.To)
                {
                    message.To.Add(new MailboxAddress(em.Name, em.Address));
                }

                foreach (var em in config.CC)
                {
                    message.Cc.Add(new MailboxAddress(em.Name, em.Address));
                }

                foreach (var em in config.BCC)
                {
                    message.Bcc.Add(new MailboxAddress(em.Name, em.Address));
                }

                message.Subject = config.Subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlBody;
                bodyBuilder.TextBody = textBody;

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    if (config.Port == 25)
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => false;
                        client.Connect(config.SMTPHost, config.Port, MailKit.Security.SecureSocketOptions.None);
                    }
                    else
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect(config.SMTPHost, config.Port, MailKit.Security.SecureSocketOptions.Auto);
                    }

                    if (!string.IsNullOrEmpty(config.SMTPUsername))
                    {
                        client.Authenticate(config.SMTPUsername, config.SMTPPassword);
                    }

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}
