using Agency.Busines.ViewModel.Account;
using Agency.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.Services.Interface
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterAsync(RegisterVm registerVm);
        Task<AppUser> ValidateUserCredentialsAsync(LoginVm loginVm);
        Task CreateRoleAsync();
    }
    public class RegisterResult
    {
        public IdentityResult? Result { get; set; }
        public AppUser? User { get; set; }
    }
}
