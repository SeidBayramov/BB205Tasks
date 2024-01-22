using Agency.Busines.Exceptions.User;
using Agency.Busines.Helpers;
using Agency.Busines.Services.Interface;
using Agency.Busines.ViewModel.Account;
using Agency.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> ValidateUserCredentialsAsync(LoginVm loginVm)
        {
            var user = (await _userManager.FindByNameAsync(loginVm.UsernameOrEmail)
            ?? await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail))
            ?? null;

            if (user is null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, loginVm.Password);

            if (!result)
            {
                return null;
            }

            return user;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterVm registerVm)
        {
            AppUser user = new()
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Name = registerVm.Name,
                Surname = registerVm.Surname
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (!result.Succeeded)
            {
                return new RegisterResult
                {
                    Result = result,
                    User = null
                };
            }

            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            return new RegisterResult
            {
                Result = result,
                User = user
            };
        }

        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
