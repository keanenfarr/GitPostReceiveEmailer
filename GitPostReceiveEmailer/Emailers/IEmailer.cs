using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GitPostReceiveEmailer
{
    public interface IEmailer
    {
        void SendEmail(Configuration config, string htmlBody, string textBody);
    }
}
