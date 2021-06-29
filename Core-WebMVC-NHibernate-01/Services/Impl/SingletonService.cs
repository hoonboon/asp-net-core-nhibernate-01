using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Services.Impl
{
    [RegisterAsSingleton]
    public class SingletonService : ISingletonService
    {
        Guid id;

        public SingletonService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetOperationID()
        {
            return id;
        }

    }
}
