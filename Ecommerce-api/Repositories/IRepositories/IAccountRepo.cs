using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface IAccountRepo
    {
        Task<string> Addnewuser(RegistrationDto newuser);
        Task<string> Login(LoginModel login);
        Task<string> UpdatePassword(RegisterModel user, LoginModel login);
        
    }
}
