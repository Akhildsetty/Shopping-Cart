using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface IAccountRepo
    {
        Task<int> Addnewuser(RegistrationDto newuser);
        Task<RegisterModel> Login(LoginModel login);
        Task<int> UpdatePassword(RegisterModel user, LoginModel login);
        
    }
}
