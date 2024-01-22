using App.BUSINESS.DTOs.AppUser;
using App.BUSINESS.DTOs.Student;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(AppUserDto appUserDto);
        Task<SignInResult> LoginAsync(AppUserLoginDto appUserLoginDto);
        Task LogoutAsync();

    }
}
