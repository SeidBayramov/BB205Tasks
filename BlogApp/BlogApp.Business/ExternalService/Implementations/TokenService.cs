using BlogApp.Business.DTOs.AccountDtos;
using BlogApp.Business.ExternalService.Interface;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BlogApp.Business.ExternalService.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public TokenResponsDto CreateToken(AppUser user, int expiretdate = 60)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.GivenName,user.Name)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigninKey"]));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
             audience: _configuration["Jwt:Audience"],
             claims: claims,
             notBefore: DateTime.UtcNow,
             expires: DateTime.UtcNow.AddMinutes(expiretdate),
             signingCredentials: credentials
            );


            var tokenhandler = new JwtSecurityTokenHandler();

            string token=tokenhandler.WriteToken(jwtSecurityToken);

            return new()
            {
                Token = token,
                ExpariDate = jwtSecurityToken.ValidTo,


            };
        }
    }
}
