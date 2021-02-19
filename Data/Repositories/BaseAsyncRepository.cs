using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TechInfoLookUp.Data.Repositories
{
    /// <summary>
    /// Base generic reposity used for manipulating data.
    /// </summary>
    /// <typeparam name="TEntity">Type of the target entity.</typeparam>
    /// <typeparam name="UEntityId">
    /// Type of the id of the entity,
    /// which will be passed to <seealso cref="IIdObject{T}">IIdEntity</seealso>.
    /// </typeparam>
    public class BaseAsyncRepository<TEntity, UEntityId> : IAsyncRepository<TEntity, UEntityId>
        where TEntity : class, IIdObject<UEntityId>
    {
        protected readonly TechInfoDbContext DataContext;

        protected virtual DbSet<TEntity> DbSet { get => DataContext.Set<TEntity>(); }

        public BaseAsyncRepository(TechInfoDbContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>
        /// Get an entity by its id.
        /// </summary>
        /// <param name="id">Id.</param>
        public virtual async Task<TEntity> GetByIdAsync(UEntityId id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Get entities with only skip, take, include properties.
        /// </summary>
        /// <param name="skip">Number of elements that will be skipped (default is 0).</param>
        /// <param name="take">Number of elements that will be taken (default is 50).</param>
        /// <param name="includeProperties">Navigation properties that should be included when loading data.</param>
        /// <returns>
        /// Return a tuple that contains pagination result.
        /// <para>- Item1: item list.</para>
        /// <para>- Item2: total values.</para>
        /// </returns>
        public virtual async Task<Tuple<IQueryable<TEntity>, int>> GetAsync(
            int skip = 0,
            int take = 50,
            params Expression<Func<TEntity, object>>[] includeProperties
        )
        {
            var values = DbSet.Skip(skip).Take(take);

            if (includeProperties != null && includeProperties.Length > 0)
                foreach (var includeProperty in includeProperties)
                    values = values.Include(includeProperty);

            values = values.OrderBy(e => e.Id);
            var totalValues = await CountAsync();

            return new Tuple<IQueryable<TEntity>, int>(values, totalValues);
        }

        /// <summary>
        /// Query entities with filter, skip, take, include properties.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="skip">Number of elements that will be skipped (default is 0).</param>
        /// <param name="take">Number of elements that will be taken (default is 50).</param>
        /// <param name="includeProperties">Navigation properties that should be included when loading data.</param>
        /// <returns>
        /// Return a tuple that contains pagination result.
        /// <para>- Item1: item list.</para>
        /// <para>- Item2: total values.</para>
        /// </returns>
        public virtual async Task<Tuple<IQueryable<TEntity>, int>> QueryAsync(
            Expression<Func<TEntity, bool>> predicate,
            int skip = 0,
            int take = 50,
            params Expression<Func<TEntity, object>>[] includeProperties
        )
        {
            if (predicate == null)
                return await GetAsync(skip, take, includeProperties);

            var where = DbSet.Where(predicate);
            var values = where.Skip(skip).Take(take);

            if (includeProperties != null && includeProperties.Length > 0)
                foreach (var includeProperty in includeProperties)
                    values = values.Include(includeProperty);

            values = values.OrderBy(e => e.Id);
            var totalValues = await where.CountAsync();

            return new Tuple<IQueryable<TEntity>, int>(values, totalValues);
        }

        /// <summary>
        /// Count number of entities.
        /// </summary>
        public virtual async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        /// <summary>
        /// Add new entity into database.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Added entity.</returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = DbSet.Add(entity);
            await DataContext.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">New entity to override.</param>
        /// <returns>Update successfully or not.</returns>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            return await DataContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">Target entity to delete.</param>
        /// <returns>Delete successfully or not.</returns>
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return await DataContext.SaveChangesAsync() > 0;
        }
    }
}
