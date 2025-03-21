using AutoMapper;
using NetCore7.Common;
using NetCore7.Core;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities;
using NetCore7.Core.Entities.Security;
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
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IContextProvider contextProvider, IMapper mapper) {
            _unitOfWork = unitOfWork; 
            _contextProvider = contextProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserListDto>> GetAll()
        {
            var users = await _unitOfWork.Users.GetProjectedMany(null,
                 x => new UserListDto()
                 {
                     Id = x.Id,
                     Email= x.Email,
                     FullName= x.FullName,  
                     UserRoles = string.Join(",", x.UserRoles.Select(z => z.Role.Name))
        });
            return users;

        }
        public async Task Add(UserAddEditDto dto)
        {
            if (dto.Email == "" || dto.FullName == "") throw new Exception("Debe ingresar un valor!"); 
            User entity;            
             entity = await _unitOfWork.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null) throw new Exception("Ya existe el usuario!");

            entity = new User()
            {
                Email = dto.Email,
                FullName = dto.FullName
            };
            var roles = new List<UserRoles>();
            UserRoles rol;

            foreach (var id in dto.RolesIds)
            {
                rol = new UserRoles();
                rol.UserId = entity.Id;
                rol.RoleId = id;

                roles.Add(rol);
            }
            entity.UserRoles = roles;

            await _unitOfWork.Users.Add(entity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Update(UserAddEditDto dto)
        {
            var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null && dto.Id != entity.Id) throw new Exception("Ya existe el usuario!");
            var rolesIdsDb = entity.UserRoles.Select(x => x.RoleId).ToList();
            var addRolesIds = dto.RolesIds.Where(x => !rolesIdsDb.Contains(x)).ToList();
            var deleteRolesIds = rolesIdsDb.Where(x => !dto.RolesIds.Contains(x)).ToList();
            entity.FullName = dto.FullName;

            UserRoles rol;
            foreach (var id in addRolesIds)
            {
                rol = new UserRoles()
                {
                    RoleId = id
                };
                entity.UserRoles.Add(rol);
            }
            foreach (var id in deleteRolesIds)
            {
                var entityToRemove = _unitOfWork.UserRoles.FirstOrDefault(x => x.UserId == entity.Id && x.RoleId == id);
                
                _unitOfWork.UserRoles.Remove(entityToRemove);

            }            

            _unitOfWork.Users.Update(entity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(int id)
        {
           var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new Exception("Ocurrio un error en el proceso.");
           
            _unitOfWork.Users.Remove(id);
            await _unitOfWork.CommitAsync();
        }
        public async Task<UserDto> GetById(int id)
        {
            var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new Exception("Ocurrio un error al obtener usuario.");

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> GetUserByName(string email)
        {
            var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Email == email);
            if (entity == null) throw new Exception("Usuario no encontrado");

            return _mapper.Map<UserDto>(entity);
        }
    }
}
