using System;
using Microsoft.EntityFrameworkCore;
using TechInfoLookUp.Data.Configurations;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data
{
    public class TechInfoDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; private set; }
        public DbSet<Tech> Techs { get; private set; }

        public TechInfoDbContext(DbContextOptions<TechInfoDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new TagConfiguration().Config(modelBuilder.Entity<Tag>());
            new TechConfiguration().Config(modelBuilder.Entity<Tech>());
        }
    }
}
