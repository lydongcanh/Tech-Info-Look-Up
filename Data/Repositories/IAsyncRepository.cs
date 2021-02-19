using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TechInfoLookUp.Data.Repositories
{
    /// <summary>
    /// Base interface for all the reposity class.
    /// </summary>
    /// <typeparam name="TEntity">Type of the target entity.</typeparam>
    /// <typeparam name="UEntityId">
    /// Type of the id of the entity,
    /// which will be passed to <seealso cref="IIdObject{T}">IIdEntity</seealso>.
    /// </typeparam>
    public interface IAsyncRepository<TEntity, UEntityId> where TEntity : IIdObject<UEntityId>
    {
        /// <summary>
        /// Get an entity by its id.
        /// </summary>
        /// <param name="id">Id.</param>
        Task<TEntity> GetByIdAsync(UEntityId id);

        /// <summary>
        /// Get entities with only skip, take, include properties.
        /// </summary>
        /// <param name="skip">Number of elements that will be skipped (default is 0).</param>
        /// <param name="take">Number of elements that will be taken (default is 50).</param>
        /// <param name="includeProperties">Navigation properties that should be included when loading data.</param>
        /// <returns>
        /// Return a tupple that contains pagination result.
        /// - Item1: item list.
        /// - Item2: total values.
        /// </returns>
        Task<Tuple<IQueryable<TEntity>, int>> GetAsync(
            int skip = 0,
            int take = 50,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Query entities with filter, skip, take, include properties.
        /// </summary>
        /// <param name="predicate">Filter predicate.</param>
        /// <param name="skip">Number of elements that will be skipped (default is 0).</param>
        /// <param name="take">Number of elements that will be taken (default is 50).</param>
        /// <param name="includeProperties">Navigation properties that should be included when loading data.</param>
        /// <returns>
        /// Return a tupple that contains pagination result.
        /// - Item1: item list.
        /// - Item2: total values.
        /// </returns>
        Task<Tuple<IQueryable<TEntity>, int>> QueryAsync(
            Expression<Func<TEntity, bool>> predicate, 
            int skip = 0, 
            int take = 50,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        /// <summary>
        /// Count number of entities.
        /// </summary>
        Task<int> CountAsync();

        /// <summary>
        /// Add new entity into database.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <returns>Added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">New entity to override.</param>
        /// <returns>Update successfully or not.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">Target entity to delete.</param>
        /// <returns>Delete successfully or not.</returns>
        Task<bool> DeleteAsync(TEntity entity);
    }
}
