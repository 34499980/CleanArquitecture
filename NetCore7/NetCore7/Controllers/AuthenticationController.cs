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


        private const string SECRET_KEY = "asdwda1d8a4sd8w4das8d*w8d*asd@#";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public AuthenticationController(IConfiguration configuration, IAuthService authService, IUserService userService)
        {
            this._configuration = configuration;
            this._authService = authService;
            this._userService = userService;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public async Task <IActionResult> Post(UserDto userInput)
        {

            try
            {              

               
                UserDto userOutput = await _userService.GetUserByName(userInput.Email);

                if (userOutput != null && userInput.Password == userOutput.Password)
                {
                   
                    var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
                    var header = new JwtHeader(credentials);
                    DateTime expiry = DateTime.UtcNow.AddMinutes(60);
                    int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;
                    var payload = new JwtPayload
                    {
                        {"id", userOutput.Id },
                        { "email", userOutput.Email},
                        { "exp", ts},
                        { "iss", "https://localhost:44362"},
                        { "aud", "https://localhost:44362"}
                    };
                    var secToken = new JwtSecurityToken(header, payload);
                    var handler = new JwtSecurityTokenHandler();
                    var tokenString = handler.WriteToken(secToken);

                    userOutput.Token = tokenString;

                    return Ok(userOutput);


                }
                else
                {
                    return StatusCode(401, "Usuario o contraseña incorrectos.");

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
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
