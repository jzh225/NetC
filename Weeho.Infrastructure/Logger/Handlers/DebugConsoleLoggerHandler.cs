using Weeho.Infrastructure.Logger.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Logger.Handlers
{
    public class DebugConsoleLoggerHandler : ILoggerHandler
    {
        private readonly ILoggerFormatter _loggerFormatter;

        public DebugConsoleLoggerHandler() : this(new DefaultLoggerFormatter()) { }

        public DebugConsoleLoggerHandler(ILoggerFormatter loggerFormatter)
        {
            _loggerFormatter = loggerFormatter;
        }

        public void Publish(LogMessage logMessage)
        {
            System.Diagnostics.Debug.WriteLine(_loggerFormatter.ApplyFormat(logMessage));
        }
    }
}
