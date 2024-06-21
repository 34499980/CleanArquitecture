using NetCore7.Core;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContextProvider _contextProvider;

        public UserService(IUnitOfWork unitOfWork, IContextProvider contextProvider) {
            _unitOfWork = unitOfWork; 
            _contextProvider = contextProvider;
        }
    }
}
