using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechInfoLookUp.Data.Entities;

namespace TechInfoLookUp.Data.Configurations
{
    public class TechTagConfiguration : IEntityConfiguration<TechTag>
    {
        public void Config(EntityTypeBuilder<TechTag> builder)
        {
            builder.Ignore(tt => tt.Id);
            builder.HasKey(tt => new { tt.TechId, tt.TagId });

            builder.HasOne(tt => tt.Tag)
                   .WithMany(t => t.TechTags)
                   .HasForeignKey(ttt => ttt.TagId);

            builder.HasOne(tt => tt.Tech)
                   .WithMany(t => t.TechTags)
                   .HasForeignKey(ttt => ttt.TechId);
        }
    }
}
