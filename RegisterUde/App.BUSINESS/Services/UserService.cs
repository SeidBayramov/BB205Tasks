using App.BUSINESS.DTOs.AppUser;
using App.CORE.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(AppUserDto appUserDto)
        {
            var user = new AppUser { UserName = appUserDto.Username, Email = appUserDto.Email };
            var result = await _userManager.CreateAsync(user, appUserDto.Password);

            return result;
        }

        public async Task<SignInResult> LoginAsync(AppUserLoginDto appUserLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(appUserLoginDto.Username, appUserLoginDto.Password, appUserLoginDto.RememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
