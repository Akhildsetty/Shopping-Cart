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
        public async Task<string> Addnewuser(RegistrationDto newuser)
        {
            try
            {
                
                await _dbcontext.Registration.AddAsync(
                    new RegisterModel{
                    
                    FirstName=newuser.FirstName,
                    LastName=newuser.LastName,
                    Email=newuser.Email,
                    Password=newuser.Password,
                    PhoneNumber=newuser.PhoneNumber
                   
                });
                var adduser = await _dbcontext.SaveChangesAsync();
                return adduser != 0 ? "true" : "false";

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        

        public async Task<string> Login(LoginModel login)
        {
            try
            {
               var user = await _dbcontext.Registration.Where(x=>x.Email==login.Email).FirstOrDefaultAsync();
                if(user != null)
                {
                    return user.Password==login.Password ? "true" : "false";
                }
                return "false";

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdatePassword(RegisterModel user,LoginModel login)
        {
            try
            {
                
                if(user != null)
                {
                   user.Password = login.Password;
                    var resetpassword = await _dbcontext.SaveChangesAsync();
                    return resetpassword != 0 ? "true" : "false";
                }
                return "false";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
