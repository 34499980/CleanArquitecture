using Microsoft.EntityFrameworkCore;
using NetCore7.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Infrastructure.Data
{
    public abstract class BaseUnitOfWork: IBaseUnitOfWork, IDisposable
    {
        protected readonly DefaultContext _context;
        protected BaseUnitOfWork(DefaultContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
