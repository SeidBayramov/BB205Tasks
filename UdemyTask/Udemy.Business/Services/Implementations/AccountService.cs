using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Udemy.Business.DTOs.AccountDtos;
using Udemy.Business.Exceptions.Account;
using Udemy.Business.ExternalService.Interface;
using Udemy.Business.Services.Interfaces;
using Udemy.Core.Entities;
using Udemy.DAL.Context;
using Udemy.DAL.Repositories.Interfaces;

namespace Udemy.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
            var user = await _userManager.FindByNameAsync(dto.UserNameorEmail)?? await _userManager.FindByEmailAsync(dto.UserNameorEmail);

            if(user == null)
            {
                throw new UserNotFoundException();
            }
            if(await _userManager.CheckPasswordAsync(user, dto.Password)) { throw new UserNotFoundException(); }

            return _tokenService.CreateToken(user);


        }

    }
}
