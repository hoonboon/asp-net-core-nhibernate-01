using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Services
{
    public interface IScopedService
    {
        Guid GetOperationID();
    }
}
