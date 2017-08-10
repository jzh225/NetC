using Weeho.Infrastructure.Logger.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Weeho.Infrastructure.Logger.Handlers
{
    public class ConsoleLoggerHandler : ILoggerHandler
    {
        private readonly ILoggerFormatter _loggerFormatter;

        public ConsoleLoggerHandler() : this(new DefaultLoggerFormatter()) { }

        public ConsoleLoggerHandler(ILoggerFormatter loggerFormatter)
        {
            _loggerFormatter = loggerFormatter;
        }

        public void Publish(LogMessage logMessage)
        {
            Console.WriteLine(_loggerFormatter.ApplyFormat(logMessage));
        }
    }
}
