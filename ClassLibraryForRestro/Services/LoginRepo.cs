using ClassLibraryForRestro.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Restaurant.Areas.Identity.Data;
using Restaurant.Areas.Identity.Pages.Account;
using System.Threading.Tasks;

namespace ClassLibraryForRestro.Services
{
    public class LoginRepo : ILogin
    {

       
        private readonly UserManager<RestaurantUser> _user;
        public LoginRepo(UserManager<RestaurantUser> user)
        {
           
            _user = user;
        }
        public async Task<LoginModel.InputModel> CheckAsync(LoginModel.InputModel inputModel)
        {
            var user = await _user.FindByNameAsync(inputModel.Email);
            if (user != null && await _user.CheckPasswordAsync(user, inputModel.Password))
            {
                return inputModel;
            }
                
            return null;
        }
        
    }
}
