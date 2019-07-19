using System.Collections.Generic;

namespace GitPostReceiveEmailer
{
    public class Configuration
    {
        public EmailAddress From { get; set; }
        public IList<EmailAddress> To { get; set; }
        public IList<EmailAddress> CC { get; set; }
        public IList<EmailAddress> BCC { get; set; }
        public string SMTPHost { get; set; }
        public int Port { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string Subject { get; set; }
        public IList<string> RepositoryPaths { get; set; }
    }
}
