using ApplicationCore.Model.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public interface IAccountService
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> LoginAsync(LoginModel loginModel);
    }
}
