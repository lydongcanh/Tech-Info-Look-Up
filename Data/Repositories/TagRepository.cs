using System;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Repositories
{
    public class TagRepository : BaseAsyncRepository<Tag, int>
    {
        public TagRepository(TechInfoDbContext dbContext) : base(dbContext) { }
    }
}
