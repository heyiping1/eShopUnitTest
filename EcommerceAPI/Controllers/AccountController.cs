using ApplicationCore;
using ApplicationCore.Model.Request;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _accountService.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok("Account has been created successfully");

            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _accountService.LoginAsync(model);
            return Ok(result);
        }
    }
}
