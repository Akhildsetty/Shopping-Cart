using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;

namespace Ecommerce_api.Repositories.IRepositories
{
    public interface IAccountRepo
    {
        Task<string> Addnewuser(RegistrationDto newuser);
        Task<Users> Login(LoginModel login);
        Task<int> UpdatePassword(Users user, LoginModel login);

        Task<string> ValidateOtp(string email, string otp);
        Task<int> SendOtp( string email,Users user);
        Task<int> DeleteUserbyEmail(Users user);

    }
}
