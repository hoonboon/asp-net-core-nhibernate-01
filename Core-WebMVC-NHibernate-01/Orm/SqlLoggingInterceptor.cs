using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Orm
{
    public class SqlLoggingInterceptor : EmptyInterceptor
    {
        private readonly ILogger<INHSession> _logger;

        public SqlLoggingInterceptor(ILogger<INHSession> logger)
        {
            _logger = logger;
        }

        public override SqlString OnPrepareStatement(SqlString sql)
        {
            if (_logger != null)
            {
                _logger.LogDebug(sql.ToString());
            }

            return base.OnPrepareStatement(sql);
        }

    }
}
