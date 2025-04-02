using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NetCore7.Common;
using NetCore7.Core;
using NetCore7.Core.Dtos;
using NetCore7.Core.Entities;
using NetCore7.Core.Entities.Security;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using NetCore7.Core.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCore7.Core.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContextProvider _contextProvider;
        private readonly IMapper _mapper;
        private const string SECRET_KEY = "asdwda1d8a4sd8w4das8d*w8d*asd@#";


        public AuthService(IUnitOfWork unitOfWork, IContextProvider contextProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextProvider = contextProvider;
            _mapper = mapper;
        }
       
        public void SetAuthorization(UserDto dto)
        {
            try
            {
                _contextProvider.RoleIds = dto.RolesIds;
                //.SelectedCountry = dto.IdCountry;
                _contextProvider.UserId = dto.Id;
                _contextProvider.PermissionsIds = dto.PermissionsIds;
                _contextProvider.Email = dto.Email;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateToken(int userId, string userName)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7169",
                audience: "https://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenerateKey()
        {
            using (var random = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32];
                random.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }

}
