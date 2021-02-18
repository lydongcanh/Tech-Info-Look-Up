using System;
using Microsoft.EntityFrameworkCore;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data
{
    public class TechInfoDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; private set; }

        public TechInfoDbContext(DbContextOptions<TechInfoDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
