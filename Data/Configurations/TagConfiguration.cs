using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Configurations
{
    public class TagConfiguration : IEntityConfiguration<Tag>
    {
        public void Config(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.HasMany(t => t.TechTags)
                   .WithOne(tt => tt.Tag)
                   .HasForeignKey(ttt => ttt.TagId);
        }
    }
}
