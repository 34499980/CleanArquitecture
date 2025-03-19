using NetCore7.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Repositories
{
    public interface IRepository<TEntity, TIdentifier> where TEntity: Entity<TIdentifier>
    {      
        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TIdentifier id);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? predicate);
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, bool>>[]? predicates);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsNoTrack();
        Task<TEntity> Get(TIdentifier id);
        Task<bool> Any(Expression<Func<TEntity, bool>>? predicates);
        Task<IEnumerable<TProjected>> GetProjectedMany<TProjected>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection, bool noTracking = false);
        Task<PageResult<TProjected>> GetPaged<TProjected>(int pageIndex, int pageSize, string sortExpression, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection);
    }
}
