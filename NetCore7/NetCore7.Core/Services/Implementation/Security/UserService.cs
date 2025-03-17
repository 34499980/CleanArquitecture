using AutoMapper;
using NetCore7.Common;
using NetCore7.Core;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities;
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

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll(); 
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task Add(UserDto dto)
        {
            User entity;            
             entity = await _unitOfWork.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null) throw new Exception("Ya existe el usuario!");

            entity = _mapper.Map<User>(dto);
            await _unitOfWork.Users.Add(entity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Update(UserDto dto)
        {
            var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (entity != null) throw new Exception("Ya existe el usuario!");
            entity.FullName = dto.FullName;
            entity.UserRoles = dto.UserRoles;

            _unitOfWork.Users.Update(entity);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(int id)
        {
           var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new Exception("Ocurrio un error en el proceso.");
        }
        public async Task<UserDto> GetById(int id)
        {
            var entity = await _unitOfWork.Users.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new Exception("Ocurrio un error al obtener usuario.");

            return _mapper.Map<UserDto>(entity);
        }
    }
}
