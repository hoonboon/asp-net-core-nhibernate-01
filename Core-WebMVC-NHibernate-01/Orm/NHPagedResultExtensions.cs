using Core_WebMVC_NHibernate_01.Dto;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebMVC_NHibernate_01.Orm
{
    public static class NHPagedResultExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(
            this IQueryable<T> query, int pageNo, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = pageNo,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (pageNo - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

    }
}
