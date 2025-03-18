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
    public class UserRoleConfiguration: IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {           

            // Mapping for table
            builder.ToTable("UserRoles");


            builder.Property(t => t.RoleId).IsRequired();
            builder.Property(t => t.UserId).IsRequired();

            // Primary Key
            builder.HasKey(p => new { p.RoleId, p.UserId });

            // Relationships
            builder.HasOne(p => p.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
