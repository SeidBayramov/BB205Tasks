using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Udemy.Business.DTOs.AccountDtos;
using Udemy.Business.Services.Interfaces;
using Udemy.Core.Entities;
using Udemy.DAL.Context;

namespace Udemy.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] AccountRegisterDto registerDto)
        {
            var result = await _accountService.Register(registerDto);
            if (result.Succeeded)
            {
                return Ok("Registrated successfully!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm]LoginDto dto)
        {
            var result = await _accountService.LoginAsync(dto);
            return Ok(result);
        }
    }
}

