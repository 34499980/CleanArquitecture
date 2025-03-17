using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities;
using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, DefaultContext context)
        {
            context.Database.EnsureCreated(); 

        }
        public static void Seeds(ModelBuilder builder) {
            SeedModules(builder);
            SeedRoles(builder);
            SeedPerissions(builder);
            SeedPermissionRoles(builder);
            SeedUSers(builder);
        }
        private static void SeedModules(ModelBuilder builder)
        {
            builder.Entity<Module>().HasData(new List<Module>()
            {
                new Module(){Id = (int)Core.Enums.Modules.Home, Name = Core.Enums.Modules.Home.ToString(), Code = Core.Enums.Modules.Home.ToString(), Description = Core.Enums.Modules.Home.ToString(), Active = true, ApplicationId = 1 ,},
                new Module(){Id = (int)Core.Enums.Modules.Settings, Name = Core.Enums.Modules.Settings.ToString(), Code =Core.Enums.Modules.Settings.ToString(), Description = Core.Enums.Modules.Settings.ToString(), Active = true, ApplicationId = 1}
            });
        }
        private static void SeedPerissions(ModelBuilder builder)
        {
            builder.Entity<Permission>().HasData(new List<Permission>()
            {
                new Permission(){Id = (int)Core.Enums.Permissions.CreaateUser, Name = Core.Enums.Permissions.CreaateUser.ToString(), ModuleId = (int)Core.Enums.Modules.Settings},
                new Permission(){Id = (int)Core.Enums.Permissions.EditUser, Name = Core.Enums.Permissions.EditUser.ToString(), ModuleId = (int)Core.Enums.Modules.Settings},
                new Permission(){Id = (int)Core.Enums.Permissions.ViewUser, Name = Core.Enums.Permissions.ViewUser.ToString(), ModuleId = (int)Core.Enums.Modules.Settings},
                new Permission(){Id = (int)Core.Enums.Permissions.DeleteUser, Name = Core.Enums.Permissions.DeleteUser.ToString(), ModuleId = (int)Core.Enums.Modules.Settings}
            });
        }
        private static void SeedRoles(ModelBuilder builder) 
        {
            builder.Entity<Role>().HasData(new List<Role>()
            {
                new Role(){Id = (int)Core.Enums.Roles.Administrator, Name = Core.Enums.Roles.Administrator.ToString(), Description = Core.Enums.Roles.Administrator.ToString()},
                new Role(){Id = (int)Core.Enums.Roles.Operador, Name = Core.Enums.Roles.Operador.ToString(), Description = Core.Enums.Roles.Operador.ToString()}
            });
        }
        private static void SeedPermissionRoles(ModelBuilder builder)
        {
            builder.Entity<RolePermission>().HasData(new List<RolePermission>()
            {
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.ViewUser, RoleId = (int)Core.Enums.Roles.Administrator},
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.CreaateUser, RoleId = (int)Core.Enums.Roles.Administrator},
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.EditUser, RoleId = (int)Core.Enums.Roles.Administrator},
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.DeleteUser, RoleId = (int)Core.Enums.Roles.Administrator},

                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.ViewUser, RoleId = (int)Core.Enums.Roles.Operador},
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.CreaateUser, RoleId = (int)Core.Enums.Roles.Operador},
                new RolePermission(){ PermissionId = (int)Core.Enums.Permissions.EditUser, RoleId = (int)Core.Enums.Roles.Operador}

            });
        }
        private static void SeedUSers(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new List<User>()
            {
                new User(){Id = 1,
                          FullName = "admin",
                          Email =  "admin@gmail.com",
                          UserRoles = new List<UserRoles>() {
                          new UserRoles(){
                          UserId = 1,
                          RoleId = (int)Core.Enums.Roles.Administrator}
                           },
                }
            });
        }
    }
}
