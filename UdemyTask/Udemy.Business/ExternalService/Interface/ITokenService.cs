using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.AccountDtos;
using Udemy.Core.Entities;

namespace Udemy.Business.ExternalService.Interface
{
    public interface ITokenService
    {
        TokenResponsDto CreateToken(AppUser user, int expiretdate=60);

    }
}
