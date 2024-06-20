using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore7.Common;

namespace NetCore7.Infrastructure.Data.Configurations.Base
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Set key for entity
            builder.HasKey(p => p.Id);
        }
    }
}
