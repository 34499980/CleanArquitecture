using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCore7.Core.Dtos;
using NetCore7.Core.Services;
using NetCore7.Core.Services.Contracts.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore7.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;


        public AuthenticationController(IConfiguration configuration, IAuthService authService, IUserService userService)
        {
            this._configuration = configuration;
            this._authService = authService;
            this._userService = userService;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public async Task <UserDto> Post(AuthDto userInput)
        {

            try
            {              

               
                UserDto userOutput = await _userService.GetUserByName(userInput.Email);

                if (userOutput != null && userInput.Password == userOutput.Password)
                {
                   userOutput.Token = _authService.GenerateToken(userOutput.Id, userOutput.Email);
                    /* var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
                     var header = new JwtHeader(credentials);
                     DateTime expiry = DateTime.UtcNow.AddMinutes(60);
                     int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;
                     var payload = new JwtPayload
                     {
                         {"id", userOutput.Id },
                         { "email", userOutput.Email},
                         { "exp", ts},
                         { "iss", "https://localhost:7169"},
                         { "aud", "https://localhost:4200"}
                     };
                     var secToken = new JwtSecurityToken(header, payload);
                     var handler = new JwtSecurityTokenHandler();
                     var tokenString = handler.WriteToken(secToken);

                     userOutput.Token = tokenString;

                     return Ok(userOutput);

                     */

                    return userOutput;
                }
                else
                {
                    throw new Exception("Usuario o contraseña incorrectos.");

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("SetAuthorization")]
        public void SetAuthorization(UserDto dto)
        {

            try
            {
                _authService.SetAuthorization(dto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
