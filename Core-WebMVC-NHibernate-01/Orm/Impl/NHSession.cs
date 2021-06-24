using Core_WebMVC_NHibernate_01.Models.Books;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Orm.Impl
{
    public class NHSession : INHSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHSession(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task DeleteAsync<T>(T entity)
        {
            await _session.DeleteAsync(entity);
        }

        public IQueryable<T> GetQueryableData<T>()
        {
            return _session.Query<T>();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveAsync<T>(T entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task RunInTransactionAsync(Action action)
        {
            try
            {
                BeginTransaction();
                action();
                await CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }


        public async Task<T> RunInTransactionAsync<T>(Func<Task<T>> func)
        {
            try
            {
                BeginTransaction();
                var retval = await func();
                await CommitAsync();
                return retval;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

    }
}
