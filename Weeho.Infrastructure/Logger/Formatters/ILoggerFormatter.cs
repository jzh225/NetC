using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Logger.Formatters
{
    public interface ILoggerFormatter
    {
        string ApplyFormat(LogMessage logMessage);
    }
}
