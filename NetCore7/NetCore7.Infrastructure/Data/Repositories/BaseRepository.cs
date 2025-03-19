using Microsoft.EntityFrameworkCore;
using NetCore7.Common;
using NetCore7.Core.Repositories;
using NetCore7.Infrastructure.Data;
using NetCore7.Infrastructure.Data.Repositories.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace NetCore7.Infrastructure
{
    public abstract class BaseRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : Entity<TIdentifier>
    {
        protected readonly DefaultContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DefaultContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>>? predicates)
        {
            return await ApplyFilters(_dbSet, predicates).AnyAsync();
        }

        public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? predicate)
        {
            return await ApplyFilters(LoadRelations(_dbSet), predicate).FirstOrDefaultAsync();

        }

        public async Task<TEntity> Get(TIdentifier id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                await LoadReferences(entity);
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, bool>>[]? predicates)
        {
            return await ApplyFilters(LoadRelations(_dbSet), predicates).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await LoadRelations(_dbSet).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsNoTrack()
        {
            return await LoadRelations(_dbSet).AsNoTracking().ToListAsync();
        }


        public void Remove(TIdentifier id)
        {
            var entity = _context.Find<TEntity>(id);
            if (entity != null) _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        protected virtual IQueryable<TEntity> LoadRelations(IQueryable<TEntity> query) 
        {
            return query;
        }
        protected IQueryable<T> ApplyFilters<T>(IQueryable<T> source, Expression<Func<T, bool>>[]? filters) where T : Entity<TIdentifier>
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    source = source.Where(filter);
                }
            }
            return source;
        }
        protected IQueryable<T> ApplyFilters<T>(IQueryable<T> source, Expression<Func<T, bool>>? filter) where T : Entity<TIdentifier>
        {
            if (filter != null)
            {
                source = source.Where(filter);
            }
            return source;
        }
        protected virtual Task<TEntity> LoadReferences(TEntity entity)
        {
            return Task.FromResult(entity);
        }
        public async Task<IEnumerable<TProjected>> GetProjectedMany<TProjected>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection, bool noTracking = false)
        {
            var query = _dbSet.AsQueryable();
            if (noTracking)
                query = _dbSet.AsNoTracking();

            return await ApplyFilter(query, predicate).Select(projection).ToListAsync();
        }
        protected IQueryable<T> ApplyFilter<T>(IQueryable<T> source, Expression<Func<T, bool>> filter) where T : Entity<TIdentifier>
        {
            if (filter != null)
            {
                source = source.Where(filter);
            }      
            return source;
        }
        public virtual async Task<PageResult<TProjected>> GetPaged<TProjected>(int pageIndex, int pageSize, string sortExpression, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjected>> projection)
        {
            return await _dbSet.GetPaged(pageIndex, pageSize, sortExpression, predicate, projection);
        }

    }
}
