using NetCore7.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Repositories.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
