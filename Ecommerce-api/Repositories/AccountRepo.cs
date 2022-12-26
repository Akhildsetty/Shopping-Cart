using Dapper;
using Ecommerce_api.Data;
using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_api.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly databaseContext _dbcontext;
        public readonly DapperContext _dapperContext;

        public AccountRepo(databaseContext dbcontext, DapperContext dapperContext)
        {
            _dbcontext = dbcontext;
            _dapperContext = dapperContext;

        }
        public async Task<int> Addnewuser(RegistrationDto newuser)
        {
            try
            {

                await _dbcontext.Registration.AddAsync(
                    new RegisterModel
                    {

                        FirstName = newuser.FirstName,
                        LastName = newuser.LastName,
                        Email = newuser.Email,
                        Password = newuser.Password,
                        PhoneNumber = newuser.PhoneNumber

                    });
                var adduser = await _dbcontext.SaveChangesAsync();
                return adduser  ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                var user = await _dbcontext.Registration.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdatePassword(RegisterModel user, LoginModel login)
        {
            try
            {
                user.Password = login.Password;
                var resetpassword = await _dbcontext.SaveChangesAsync();
                return resetpassword ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
