using System.Net.Mail;

namespace GitPostReceiveEmailer
{
    /// <summary>
    /// Sends an email via the System.Net.Mail client (which is now obsolete)
    /// </summary>
    public class SMTPClientEmailer : IEmailer
    {
        /// <summary>
        /// Sends an email via the System.net.Mail client (which is now marked obsolete)
        /// </summary>
        /// <param name="config">The configuration object.</param>
        /// <param name="htmlBody">The body of the email in HTML format.</param>
        /// <param name="textBody">The body of the email in text format (optional).</param>
        public void SendEmail(Configuration config, string htmlBody, string textBody)
        {
            if (config != null)
            {

                var mm = new MailMessage();
                mm.From = new MailAddress(string.Format("{0} <1>", config.From.Name, config.From.Address));
                mm.Subject = !string.IsNullOrEmpty(config.Subject) ? config.Subject : string.Empty;

                foreach (var em in config.To)
                {
                    mm.To.Add(new MailAddress(em.Address, em.Name));
                }

                foreach (var em in config.CC)
                {
                    mm.CC.Add(new MailAddress(em.Address, em.Name));
                }

                foreach (var em in config.BCC)
                {
                    mm.Bcc.Add(new MailAddress(em.Address, em.Name));
                }

                //Send HTML content as HTML if it is available.
                if (!string.IsNullOrEmpty(htmlBody))
                {
                    if (!string.IsNullOrEmpty(textBody))
                    {
                        var textView = AlternateView.CreateAlternateViewFromString(textBody, null, "text/plain");
                        mm.AlternateViews.Add(textView);
                    }

                    if (!string.IsNullOrEmpty(htmlBody))
                    {
                        var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
                        mm.AlternateViews.Add(htmlView);
                    }
                }
                else if (!string.IsNullOrEmpty(textBody))
                {
                    mm.Body = textBody;
                }

                var client = new SmtpClient(config.SMTPHost, config.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                if (!string.IsNullOrEmpty(config.SMTPUsername))
                {
                    client.Credentials = new System.Net.NetworkCredential(config.SMTPUsername, config.SMTPPassword);
                }

                client.Send(mm);
            }
        }
    }
}
