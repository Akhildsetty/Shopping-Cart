using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_api.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountRepo _acctrepo;
        private readonly ISharedRepo _sharedrepo;
        private readonly IConfiguration _config;

        public AccountController(IAccountRepo acctrepo, ISharedRepo sharedRepo, IConfiguration config)
        {
            _acctrepo = acctrepo;
            _sharedrepo = sharedRepo;
            _config = config;
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<IActionResult> Addnewuser(RegistrationDto newmodel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var newuser = await _acctrepo.Addnewuser(newmodel);
                    if (newuser == "Registration Successfull"|| newuser == "Account Re-Activated Successfully")
                    {
                        return Ok(new { StatusCode = 200, message = newuser });
                    }

                    return Problem(newuser);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email) && string.IsNullOrEmpty(login.Password))

                    return BadRequest("PLease fill the all details");

                if (ModelState.IsValid)
                {
                    var user = await _acctrepo.Login(login);
                    if (user != null && !user.isRemoved)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuthentication:Secretkey"]));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokeOptions = new JwtSecurityToken(
                            issuer: _config["JwtAuthentication:issuer"],
                            audience: _config["JwtAuthentication:audience"],
                            claims: new List<Claim>()
                            {
                                new Claim("FirstName",user.FirstName),
                                new Claim("LastName",user.LastName),
                                new Claim("Email",user.Email),
                                new Claim("PhoneNumber",user.PhoneNumber),
                                new Claim("Role",user.Role),
                                new Claim("AccountNumber",user.AccountNumber)
                            },
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: signinCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                        return Ok(new
                        {
                            StatusCode = 200,
                            Token = tokenString,
                            message = "Login Successfully",
                            FullName = $"{user.FirstName} {user.LastName}",
                            email = user.Email,
                            role = user.Role
                        });
                    }
                    return Unauthorized("Invalid Username or Password");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> Updatepassword(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _sharedrepo.GetuserbyEmail(login.Email);
                    if (user == null)
                        return BadRequest("false");
                    if (user != null)
                    {
                        var result = await _acctrepo.UpdatePassword(user, login);
                        if (result != 0)
                        {
                            return Ok(new { message = "Password Updated Successfully" });
                        }
                        return Problem("Password failed to Update");

                    }
                    return Unauthorized();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("sendOtpbyEmail/{email}")]
        public async Task<IActionResult> SendOTPByEmail(string email)
        {
            try
            {
                if (email == null)
                    return BadRequest("enter valid input");
                var user = await _sharedrepo.GetuserbyEmail(email);

                if (user != null)
                {
                    var result = await _acctrepo.SendOtp(email, user);
                    if (result != 0)
                    {
                        return Ok(new { message = "OTP Sent Successfully" });
                    }
                    return Problem("OTP failed to Send");
                }
                return Problem("Email doesn't Exist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP(OtpValidationDto validationDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(validationDto.otp))
                {

                    var otpadded = await _acctrepo.ValidateOtp(validationDto.email, validationDto.otp);

                    if (otpadded == "OTP Valid")
                    {
                        return Ok(new { message = "OTP Validated Successfully" });
                    }
                    return Problem("OTP failed to Validate");

                }
                return BadRequest("OTP is requried");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("deletebyemail/{email}")]
        public async Task<IActionResult> Deletebyemail(string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _sharedrepo.GetuserbyEmail(email);
                    var result = await _acctrepo.DeleteUserbyEmail(user);
                    if (result != 0)
                    {
                        return Ok(new { message = "Account Deleted Successfully" });
                    }
                    return BadRequest("Failed to Delete user");
                }
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut]
        [Route("updateProfile")]
        public async Task<IActionResult> UpdateUser([FromBody] Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _acctrepo.UpdateUser(user);
                if (result != 0)
                {
                    return Ok(new { StatusCode = 200, message = "Updated Successfully" });
                }

                return Problem("Failed to update");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
