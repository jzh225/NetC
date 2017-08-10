using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Logger
{
    public interface ILoggerHandler
    {
        void Publish(LogMessage logMessage);
    }
}
