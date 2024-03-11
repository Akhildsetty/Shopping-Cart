using Dapper;
using Ecommerce_api.Data;
using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Ecommerce_api.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly databaseContext _dbcontext;
        public readonly DapperContext _dapperContext;
        public readonly IMailServices _mailServices;
        public readonly IConfiguration _configuration;
        public readonly ISharedRepo _sharedRepo;

        public AccountRepo(databaseContext dbcontext, DapperContext dapperContext, IMailServices mailServices,IConfiguration configuration, ISharedRepo sharedRepo)
        {
            _dbcontext = dbcontext;
            _dapperContext = dapperContext;
            _mailServices = mailServices;
            _configuration = configuration;
            _sharedRepo = sharedRepo;
        }
        public async Task<string> Addnewuser(RegistrationDto newuser)
        {
            try
            {
                int adduser = 0;
                var user = await _sharedRepo.GetuserbyEmail(newuser.Email).ConfigureAwait(false);
                if (user == null)
                {
                    
                    Random generator = new Random();
                    string accountnumber = generator.Next(0, 1000000).ToString("D7");

                    var role = newuser.Role == null ? Role.Customer : newuser.Role;

                    await _dbcontext.Users.AddAsync(
                        new Users
                        {
                            AccountNumber = accountnumber,
                            FirstName = newuser.FirstName,
                            LastName = newuser.LastName,
                            Email = newuser.Email,
                            Password = newuser.Password,
                            PhoneNumber = newuser.PhoneNumber,
                            Role = role,

                        });
                     adduser = await _dbcontext.SaveChangesAsync();
                    return adduser!=0? "Registration Successfull" : "Registration Failed";
                }
                else if(user.isRemoved)
                {
                    user.isRemoved = false;
                    adduser = await _dbcontext.SaveChangesAsync();
                    return adduser != 0 ? "Registration Successfull" : "Registration Failed";

                }
                return "Email already exists";
                   

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<Users> Login(LoginModel login)
        {
            try
            {
                var user = await _dbcontext.Users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdatePassword(Users user, LoginModel login)
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

        public async Task<string> ValidateOtp(string email, string otp)
        {
            try
            {

                OtpValidation oTP_Validation = await _dbcontext.OtpValidation.Where(x=>x.Email == email&& x.Otp==otp).
                    OrderBy(x => x.Datecreated).
                    LastOrDefaultAsync();
                if (oTP_Validation != null)
                    {
                        if(oTP_Validation.Otp == otp && !oTP_Validation.Validate)
                        {

                                oTP_Validation.Validate = true;
                                var result = await _dbcontext.SaveChangesAsync();
                                

                                    return result != 0? "OTP Valid": "Invalid OTP";
                        }
                        return "Invalid OTP";
                }
                return "OTP doesn't exist ";

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<int> SendOtp(string email, Users user)
        {
            try
            {
                Random generator = new Random();
                string Otp = generator.Next(0, 1000000).ToString("D6");
                bool validate = false;
                DateTime date = DateTime.Now;
                
                using (var connection=_dapperContext.CreateConnection())
                {
                    string query = $"Insert into OTPValidation(email,Otp,Validate,Datecreated) values('{email}','{Otp}','{validate}','{date}')";
                  var result=  await connection.ExecuteAsync(query).ConfigureAwait(false);
                    if (result != 0)
                    {
                        StringBuilder body = new StringBuilder();
                        body.Append(string.Format(@" <div style='color: #002468;font-size:13px'> <p>
                                      <b> You OTP</b><br /><br />
                                       <b> {0} </b> please Contact our Customercare Support <b> {1}</b>                      
                                    </p>", Otp, _configuration["EcartSupport"]));
                        string subject = "Sending Autentication OTP";
                     var mailresponse= await _mailServices.SendEmail(user.Email, user.FirstName + " " + user.LastName, subject,body.ToString());
                    }
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteUserbyEmail(Users user)
        {
            try
            {
               user.isRemoved = true;
                var result = await _dbcontext.SaveChangesAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
