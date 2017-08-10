using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Logger
{
    public interface ILoggerHandlerManager
    {
        ILoggerHandlerManager AddHandler(ILoggerHandler loggerHandler);
        ILoggerHandlerManager AddHandler(ILoggerHandler loggerHandler, Predicate<LogMessage> filter);

        bool RemoveHandler(ILoggerHandler loggerHandler);
    }
}
