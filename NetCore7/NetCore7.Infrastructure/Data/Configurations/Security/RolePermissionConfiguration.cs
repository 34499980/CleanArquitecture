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
    public class RolePermissionConfiguration: IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {           

            // Mapping for table
            builder.ToTable("RolesPermissions");


            builder.Property(t => t.RoleId).IsRequired();
            builder.Property(t => t.PermissionId).IsRequired();

            // Primary Key
            builder.HasKey(p => new { p.RoleId, p.PermissionId });

            // Relationships
            builder.HasOne(p => p.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
