using Core_WebMVC_NHibernate_01.Models.Books;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Orm
{
    public interface INHSession
    {
        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
        void CloseTransaction();
        Task SaveAsync<T>(T entity);
        Task DeleteAsync<T>(T entity);

        IQueryable<T> GetQueryableData<T>();

        Task RunInTransactionAsync(Action action);

        Task<T> RunInTransactionAsync<T>(Func<Task<T>> func);

    }
}
