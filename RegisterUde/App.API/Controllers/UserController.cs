using App.BUSINESS.DTOs.AppUser;
using App.BUSINESS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] AppUserDto appUserDto)
        {
            var result = await _userService.RegisterUserAsync(appUserDto);

            if (result.Succeeded)
            {
                return Ok("Registration successful");
            }

            return BadRequest(result.Errors);
        }

    }
}
