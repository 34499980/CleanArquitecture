using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore7.Core.Dtos;
using NetCore7.Core.Services;
using System.Collections;

namespace NetCore7.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserDto> Get(int id)
        {
            var result = await _userService.GetById(id);
            return result;   
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserListDto>> GetAll()
        {
            var result = await _userService.GetAll();
            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(UserAddEditDto dto)
        {            
           await _userService.Add(dto); 
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UserAddEditDto dto)
        {
            await _userService.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

    }
}
