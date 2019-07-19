using System;
using System.Collections.Generic;
using System.Text;

namespace GitPostReceiveEmailer
{
    public interface IConfigurationFactory
    {
        Configuration Create();
    }
}
