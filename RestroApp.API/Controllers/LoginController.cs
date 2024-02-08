using ClassLibraryForRestro.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Areas.Identity.Data;
using Restaurant.Areas.Identity.Pages.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text; // Import the namespace where LoginModel is defined

namespace RestroApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogin _login;
       

        public LoginController(IConfiguration config,ILogin login)
        {
            _config = config;
            _login = login;
           
        }

        private async Task<LoginModel.InputModel> Authentication(LoginModel.InputModel inputModel)
        {
            return await _login.CheckAsync(inputModel);
        }

        private string GenerateToken(LoginModel.InputModel inputModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel.InputModel inputModel)
        {
            IActionResult response = Unauthorized();
            var user_ = await Authentication(inputModel);
            if (user_ != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
