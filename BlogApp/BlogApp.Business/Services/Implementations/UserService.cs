using BlogApp.Business.DTOs.AccountDtos;
using BlogApp.Business.Exceptions.Account;
using BlogApp.Business.ExternalService.Interface;
using BlogApp.Business.Services.Interface;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(AccountRegisterDto registerDto)
        {
            AppUser user = new AppUser()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }


        public async Task<TokenResponsDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserNameorEmail) ?? await _userManager.FindByEmailAsync(dto.UserNameorEmail);

            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (await _userManager.CheckPasswordAsync(user, dto.Password)) { throw new UserNotFoundException(); }

            return _tokenService.CreateToken(user);
        }
    }
}
