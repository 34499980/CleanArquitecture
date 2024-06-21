using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore7.Core.Entities.Security;
using NetCore7.Infrastructure.Data.Configurations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data.Configurations.Security
{
    public class PermissionConfiguration : BaseEntityConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);

            // Mapping for table
            builder.ToTable("Permissions");


            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.ModuleId).IsRequired();
        }
    }
}
