using Microsoft.EntityFrameworkCore;
using NetCore7.Common;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace NetCore7.Infrastructure.Data.Repositories.Security
{
    public static class PagingExtension
    {    
        public static async Task<PageResult<TProjected>> GetPaged<T, TProjected>(this IQueryable<T> query, int page, int pageSize, string sortExpression, Expression<Func<T, bool>> predicate, Expression<Func<T, TProjected>> projection)
        {
            var result = new PageResult<TProjected>();
            result.CurrentPage = page;
            result.PageSize = pageSize;

            if (predicate != null)
                query = query.Where(predicate);

            if (!string.IsNullOrEmpty(sortExpression))
                query = query.OrderBy(sortExpression);

            result.RowCount = await query.CountAsync();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = page * pageSize;
            query = query.Skip(skip).Take(pageSize);
            result.Results = await query.Select(projection).ToListAsync();
            return result;
        }

    }
}
