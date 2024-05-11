using ApplicationCore;
using ApplicationCore.Model.Request;
using ApplicationCore.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class AccountServiceAsync : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        public AccountServiceAsync(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }
        public Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            return _accountRepository.SignUpAsync(model);
        }

        public  async Task<string> LoginAsync(LoginModel loginModel)
        {
         var loginResult= await _accountRepository.LoginAsync(loginModel);
            if(loginResult.Succeeded)
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,loginModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssure"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires:DateTime.Now.AddDays(1),
                    claims:authClaim,
                    signingCredentials: new SigningCredentials(authSignKey,SecurityAlgorithms.HmacSha256Signature)
                    
                    );
                var handler = new JwtSecurityTokenHandler().WriteToken(token);
                return handler;
            }
            return null;
        }
    }
}
