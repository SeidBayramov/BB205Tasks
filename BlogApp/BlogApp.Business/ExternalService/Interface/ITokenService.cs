using BlogApp.Business.DTOs.AccountDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.ExternalService.Interface
{
    public interface ITokenService
    {
        TokenResponsDto CreateToken(AppUser user, int expiretdate=60);

    }
}
