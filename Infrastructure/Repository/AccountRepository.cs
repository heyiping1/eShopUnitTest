using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
                _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model) {
            ApplicationUser user = new ApplicationUser { 
            UserName=model.Email,
            Email= model.Email,
            FirstName= model.FirstName,
            LastName= model.LastName
            
            };
          return await  _userManager.CreateAsync(user, model.Password);
        
        }

        public async Task<SignInResult> LoginAsync(LoginModel loginModel)
        {
           return await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
        }
    }
}
