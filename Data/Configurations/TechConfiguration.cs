using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Configurations
{
    public class TechConfiguration : IEntityConfiguration<Tech>
    {
        public void Config(EntityTypeBuilder<Tech> builder)
        {
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.HasMany(t => t.TechTags)
                   .WithOne(tt => tt.Tech)
                   .HasForeignKey(ttt => ttt.TechId);
        }
    }
}
