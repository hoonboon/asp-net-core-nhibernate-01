using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/***
 * Reference: https://raw.githubusercontent.com/Bogdan-Stancescu-RBC/RBC.NHibernate.NLog/master/NLogLoggerFactory.cs
 */

namespace Core_WebMVC_NHibernate_01.Orm
{
    /// <summary>
    /// The main logger factory; this is the only class you need to use explicitly.
    /// Just call static method NHibernateLogger.SetLoggersFactory() with a
    /// NLogLoggerFactory instance during NHibernate initialization, and you're done:
    /// NHibernateLogger.SetLoggersFactory(new NLogLoggerFactory());
    /// </summary>
    public class NLogLoggerFactory : INHibernateLoggerFactory
    {
        INHibernateLogger INHibernateLoggerFactory.LoggerFor(string keyName)
        {
            return new NLogLogger(keyName);
        }

        INHibernateLogger INHibernateLoggerFactory.LoggerFor(System.Type type)
        {
            return new NLogLogger(type.FullName);
        }
    }
}
