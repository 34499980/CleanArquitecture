﻿using NetCore7.Common;
using NetCore7.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Entities
{
    public class User : Entity<int>
    {
        public User()
        {           
            UserRoles = new HashSet<UserRoles>();
           
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
