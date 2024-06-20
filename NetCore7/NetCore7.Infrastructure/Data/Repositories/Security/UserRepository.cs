using Microsoft.EntityFrameworkCore;
using NetCore7.Core.Entities;
using NetCore7.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(DefaultContext context) : base(context)
        {
        }
        protected override IQueryable<User> LoadRelations(IQueryable<User> query)
        {
           // query = query;
            return base.LoadRelations(query);
        }
    }
}
