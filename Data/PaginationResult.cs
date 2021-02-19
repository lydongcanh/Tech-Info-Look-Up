using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechInfoLookUp.Data
{
    /// <summary>
    /// Hold a pagination result when querying entities, used in:
    /// <para>- <see cref="IAsyncRepository{TEntity, UEntityId}.GetAsync(int, int)"/></para>
    /// <para>- <seealso cref="IAsyncRepository{TEntity, UEntityId}.QueryAsync(Func{TEntity, bool}, int, int)"/></para>
    /// </summary>
    public struct PaginationResult<TEntity, UEntityId> where TEntity : IIdObject<UEntityId>
    {
        public PaginationResult(List<TEntity> values, int totalValues)
        {
            Values = values;
            TotalValues = totalValues;
        }

        /// <summary>
        /// A list to hold all the queried entities.
        /// </summary>
        public List<TEntity> Values { get; set; }

        /// <summary>
        /// Total values that can be obtained.
        /// </summary>
        public int TotalValues { get; set; }
    }
}
