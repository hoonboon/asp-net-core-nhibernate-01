using NHibernate;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/***
 * Reference: https://raw.githubusercontent.com/Bogdan-Stancescu-RBC/RBC.NHibernate.NLog/master/NLogLogger.cs
 */

namespace Core_WebMVC_NHibernate_01.Orm
{
    /// <summary>
    /// This class actually links NHibernate to NLog.
    /// You shouldn't ever need to instantiate one of these directly.
    /// </summary>
    public class NLogLogger : INHibernateLogger
    {
        private readonly Logger _logger;

        private static Dictionary<NHibernateLogLevel, LogLevel> levelMapper = new Dictionary<NHibernateLogLevel, LogLevel>()
        {
            { NHibernateLogLevel.Trace, LogLevel.Trace },
            { NHibernateLogLevel.Debug, LogLevel.Debug },
            { NHibernateLogLevel.Info, LogLevel.Info },
            { NHibernateLogLevel.Warn, LogLevel.Warn },
            { NHibernateLogLevel.Error, LogLevel.Error },
            { NHibernateLogLevel.Fatal, LogLevel.Fatal },
            { NHibernateLogLevel.None, LogLevel.Off },
        };

        public NLogLogger(string logger)
        {
            _logger = LogManager.GetLogger(logger);
        }

        public bool IsEnabled(NHibernateLogLevel logLevel)
        {
            return _logger.IsEnabled(levelMapper[logLevel]);
        }

        public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
        {
            _logger.Log(levelMapper[logLevel], exception, state.Format, state.Args);
        }
    }
}
