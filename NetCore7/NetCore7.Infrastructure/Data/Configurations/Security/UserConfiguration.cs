using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities.Security;
using NetCore7.Infrastructure.Data.Configurations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCore7.Core.Entities;

namespace NetCore7.Infrastructure.Data.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // Mapping for table
            builder.ToTable("Users");

            builder.Property(t => t.FullName).IsRequired().HasMaxLength(250);
            builder.Property(t => t.Email).HasMaxLength(250);

        }
    }
}
