using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Repositories
{
    public class TechRepository : BaseAsyncRepository<Tech, int>
    {
        public TechRepository(TechInfoDbContext dbContext) : base(dbContext) { }
    }
}
