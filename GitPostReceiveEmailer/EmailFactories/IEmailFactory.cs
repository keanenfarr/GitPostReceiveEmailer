using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPostReceiveEmailer
{
    public interface IEmailGenerator
    {
        string CreateEmailText(Commit model);
    }
}
