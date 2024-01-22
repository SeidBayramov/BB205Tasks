using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.AccountDtos;
using Udemy.Core.Entities;

namespace Udemy.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(AccountRegisterDto registerDto);
        Task<TokenResponsDto> LoginAsync(LoginDto dto);
    }
}
