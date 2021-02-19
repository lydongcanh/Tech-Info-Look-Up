using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechInfoLookUp.Data.Configurations
{
    /// <summary>
    /// Base interface for all entity's configuration classes.
    /// </summary>
    /// <typeparam name="TEntity">Type of the target entity.</typeparam>
    public interface IEntityConfiguration<TEntity> where TEntity : class
    {
        void Config(EntityTypeBuilder<TEntity> builder);
    }
}
