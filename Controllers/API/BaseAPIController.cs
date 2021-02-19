using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TechInfoLookUp.Data.Repositories;
using TechInfoLookUp.Data;

namespace TechInfoLookUp.Controllers.API
{
    /// <summary>
    /// Base class for all api controllers.
    /// </summary>
    /// <typeparam name="TEntity">Type of the target entity.</typeparam>
    /// <typeparam name="UEntityId">
    /// Type of the id of the entity,
    /// which will be passed to <seealso cref="IIdEntity{T}">IIdEntity</seealso>.
    /// </typeparam>    
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseAPIController<TEntity, UEntityId> : ControllerBase
        where TEntity : class, IIdObject<UEntityId>
    {
        /// <summary>
        /// Repository class used to manipulate entity.
        /// </summary>
        protected readonly IAsyncRepository<TEntity, UEntityId> Repository;

        /// <summary>
        /// Entity's name used for sending messsage back to client.
        /// </summary>
        public abstract string EntityName { get; }

        public BaseAPIController(IAsyncRepository<TEntity, UEntityId> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// This is the basic GET method to fetch entites with pagination result.<br/>
        /// Use can simply return this method in the child controller if its GET method only need to params skip and take.
        /// </summary>
        /// <param name="skip">Number of elements that will be skipped (default is 0).</param>
        /// <param name="take">Number of elements that will be taken (default is 50).</param>
        public virtual async Task<IActionResult> GetBasicData(int skip = 0, int take = 50)
        {
            try
            {
                var result = await Repository.GetAsync(skip, take);
                return Ok(new
                {
                    success = true,
                    code = StatusCodes.Status200OK,
                    message = $"Tìm thấy thành công {result.Item2} {EntityName}.",
                    data = result
                });
            }
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    success = false,
                    code = StatusCodes.Status500InternalServerError,
                    message = $"Không tìm thấy {EntityName}. Lỗi: {e.Message}",
                    data = e.StackTrace
                });
            }
        }

        /// <summary>
        /// GET api/[controller].<br/>
        /// Get an entity by its id.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(UEntityId id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(id);
                if (entity == default(TEntity))
                    return NotFound(new
                    {
                        success = false,
                        code = StatusCodes.Status404NotFound,
                        message = $"Không tìm thấy {EntityName} với id: {id}",
                        data = new { }
                    });

                return Ok(new
                {
                    success = true,
                    code = StatusCodes.Status200OK,
                    message = $"Tìm thấy {EntityName} thành công.",
                    data = entity
                });
            } 
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    success = false,
                    code = StatusCodes.Status500InternalServerError,
                    message = $"Không tìm thấy {EntityName} với id: {id}. Lỗi: {e.Message}",
                    data = e.StackTrace
                });
            }
        }

        /// <summary>
        /// POST api/[controller].<br/>
        /// Add new entity into database.
        /// </summary>
        /// <param name="entity">Target entity.</param>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            try
            {
                var newEntity = await Repository.AddAsync(entity);
                if (newEntity == default(TEntity))
                    return BadRequest(new
                    {
                        success = false,
                        code = StatusCodes.Status400BadRequest,
                        message = $"Thêm {EntityName} không thành công.",
                        data = new { }
                    });

                return Ok(new
                {
                    success = true,
                    code = StatusCodes.Status200OK,
                    message = $"Thêm {EntityName} thành công.",
                    data = newEntity
                });
            } 
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    success = false,
                    code = StatusCodes.Status500InternalServerError,
                    message = $"Thêm {EntityName} không thành công. Lỗi: {e.Message}",
                    data = e.StackTrace
                });
            }         
        }

        /// <summary>
        /// PUT api/[controller]/id.<br/>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">Target entity.</param>
        /// <param name="id">Entity's id.</param>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(UEntityId id, [FromBody] TEntity entity)
        {
            try
            {
                var updated = await Repository.UpdateAsync(entity);
                if (!updated)
                    return BadRequest(new
                    {
                        success = false,
                        code = StatusCodes.Status400BadRequest,
                        message = $"Cập nhật {EntityName} không thành công.",
                        data = new { }
                    });

                return Ok(new
                {
                    success = true,
                    code = StatusCodes.Status200OK,
                    message = $"Cập nhật {EntityName} thành công.",
                    data = entity
                });
            }
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    success = false,
                    code = StatusCodes.Status500InternalServerError,
                    message = $"Cập nhật {EntityName} không thành công. Lỗi: {e.Message}",
                    data = e.StackTrace
                });
            }
        }

        /// <summary>
        /// DELETE api/[controller]/id.<br/>
        /// Delete an entity with its id.
        /// </summary>
        /// <param name="id">Entity's id.</param>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(UEntityId id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(id);
                if (entity == default(TEntity))
                    return NotFound(new
                    {
                        success = false,
                        code = StatusCodes.Status404NotFound,
                        message = $"Không tìm thấy {EntityName} với id: {id}",
                        data = new { }
                    });

                var deleted = await Repository.DeleteAsync(entity);
                if (!deleted)
                    return BadRequest(new
                    {
                        success = false,
                        code = StatusCodes.Status400BadRequest,
                        message = $"Xóa {EntityName} không thành công.",
                        data = new { }
                    });

                return Ok(new 
                {
                    success = true,
                    code = StatusCodes.Status200OK,
                    message = $"Xóa {EntityName} thành công.",
                    data = deleted
                });
            }
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    success = false,
                    code = StatusCodes.Status500InternalServerError,
                    message = $"Xóa {EntityName} không thành công. Lỗi: {e.Message}",
                    data = e.StackTrace
                });
            }
        }
    }
}
