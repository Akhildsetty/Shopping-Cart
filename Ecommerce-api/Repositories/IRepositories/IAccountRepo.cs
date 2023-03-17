using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface IAccountRepo
    {
        Task<int> Addnewuser(RegistrationDto newuser);
        Task<Users> Login(LoginModel login);
        Task<int> UpdatePassword(Users user, LoginModel login);
        
    }
}
