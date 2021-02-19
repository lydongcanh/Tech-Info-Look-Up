using System;
using System.Threading.Tasks;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Repositories
{
    public class TechTagRepository : BaseAsyncRepository<TechTag, Tuple<int, int>>
    {
        public TechTagRepository(TechInfoDbContext dbContext) : base(dbContext) { }

        public override async Task<TechTag> GetByIdAsync(Tuple<int, int> id)
        {
            return await DbSet.FindAsync(id.Item1, id.Item2);
        }
    }
}
