using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Services.Impl
{
    [RegisterAsScoped]
    public class ScopedService : IScopedService
    {
        Guid id;

        public ScopedService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetOperationID()
        {
            return id;
        }

    }
}
