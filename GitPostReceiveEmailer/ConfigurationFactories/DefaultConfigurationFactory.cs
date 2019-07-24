using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Mail;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GitPostReceiveEmailer
{
    public class DefaultConfigurationFactory : IConfigurationFactory
    {
        public Configuration Create()
        {
            var config = new Configuration()
            {
                From = new EmailAddress(),
                To = new List<EmailAddress>(),
                CC = new List<EmailAddress>(),
                BCC = new List<EmailAddress>(),
                RepositoryPaths = new List<string>(),
                SMTPHost = string.Empty,
                Port = 0,
                SMTPPassword = string.Empty,
                SMTPUsername = string.Empty,
                Subject = string.Empty
            };

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            config.Subject = configuration.GetSection("EmailSubject").Value;
            config.RepositoryPaths = configuration.GetSection("RepositoryPaths").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var from = configuration.GetSection("FromEmailAddress").Value;
            var to = configuration.GetSection("ToEmailAddresses").Value;
            var cc = configuration.GetSection("CCEmailAddresses").Value;
            var bcc = configuration.GetSection("BCCEmailAddresses").Value;

            var smtpSection = configuration.GetSection("Smtp");
            config.SMTPHost = smtpSection.GetSection("Server").Value;

            var port = 0;

            int.TryParse(smtpSection.GetSection("Port").Value, out port);

            config.Port = port;
            config.SMTPUsername = smtpSection.GetSection("Username").Value;
            config.SMTPPassword = smtpSection.GetSection("Password").Value;

            if (!string.IsNullOrEmpty(from))
            {
                config.From = ParseEmail(from);
            }

            if (!string.IsNullOrEmpty(to))
            {
                if (to.Contains(';') || to.Contains(','))
                {
                    try
                    {
                        string[] toAddresses = Regex.Split(to, "[,;] *");
                        toAddresses.ToList().ForEach(toAddress => config.To.Add(ParseEmail(toAddress)));
                    }
                    catch
                    {
                        config.To.Add(ParseEmail(to));
                    }
                }
                else
                {
                    config.To.Add(ParseEmail(to));
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                if (cc.Contains(';') || cc.Contains(','))
                {
                    try
                    {
                        string[] ccAddresses = Regex.Split(cc, "[,;] *");
                        ccAddresses.ToList().ForEach(ccAddress => config.CC.Add(ParseEmail(ccAddress.Trim())));
                    }
                    catch
                    {
                        config.CC.Add(ParseEmail(cc));
                    }
                }
                else
                {
                    config.CC.Add(ParseEmail(cc));
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                if (bcc.Contains(';') || bcc.Contains(','))
                {
                    try
                    {
                        string[] bccAddresses = Regex.Split(bcc, "[,;] *");
                        bccAddresses.ToList().ForEach(bccAddress => config.BCC.Add(ParseEmail(bccAddress.Trim())));
                    }
                    catch
                    {
                        config.BCC.Add(ParseEmail(bcc));
                    }
                }
                else
                {
                    config.BCC.Add(ParseEmail(bcc));
                }
            }

            return config;
        }

        EmailAddress ParseEmail(string email)
        {
            var addr = new MailAddress(email);

            return new EmailAddress()
            {
                Address = string.Format("{0}@{1}", addr.User, addr.Host),
                Name = addr.DisplayName.Trim()
            };
        }
    }
}
