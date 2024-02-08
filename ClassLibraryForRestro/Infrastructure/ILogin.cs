using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Areas.Identity.Data;
using Restaurant.Areas.Identity.Pages.Account;

namespace ClassLibraryForRestro.Infrastructure
{
    public interface ILogin
    {

         Task<LoginModel.InputModel> CheckAsync(LoginModel.InputModel inputModel);
    }
}
