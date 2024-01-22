using BlogApp.Business.DTOs.AccountDtos;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interface
{
    public interface IUserService
    { 
        Task<IdentityResult> Register(AccountRegisterDto registerDto);
        Task<TokenResponsDto> LoginAsync(LoginDto dto);
    }
}
