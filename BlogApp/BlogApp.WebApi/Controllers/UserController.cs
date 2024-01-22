using BlogApp.Business.DTOs.AccountDtos;
using BlogApp.Business.Services.Interface;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] AccountRegisterDto registerDto)
        {
            var result = await _userService.Register(registerDto);
            if (result.Succeeded)
            {
                return Ok("Registrated successfully!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            return Ok(result);
        }
    }
}







