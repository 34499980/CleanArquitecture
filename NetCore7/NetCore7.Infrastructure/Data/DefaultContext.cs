using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities;
using NetCore7.Infrastructure.Data.Configurations;
using NetCore7.Infrastructure.Data.Configurations.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data
{
    public class DefaultContext: DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ModuleConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new RolePermissionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            SeedData.Seeds(builder);
        }
    }
}
