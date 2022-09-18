using Ecommerce_api.Models;
using Ecommerce_api.Models.Dto;
using Ecommerce_api.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_api.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [ApiController]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountRepo _acctrepo;
        private readonly ISharedRepo _sharedrepo;

        public AccountController(IAccountRepo acctrepo,ISharedRepo sharedRepo)
        {
            _acctrepo = acctrepo;
            _sharedrepo = sharedRepo;
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<IActionResult> Addnewuser(RegistrationDto newmodel)
                    {
            try
            {
                if (string.IsNullOrEmpty(newmodel.FirstName) && string.IsNullOrEmpty(newmodel.LastName) &&
                    string.IsNullOrEmpty(newmodel.Email) && string.IsNullOrEmpty(newmodel.Password) && string.IsNullOrEmpty(newmodel.PhoneNumber))

                    return BadRequest("PLease fill the all details");

                if (ModelState.IsValid)
                {
                    var user = await _sharedrepo.GetuserbyEmail(newmodel.Email).ConfigureAwait(false);
                    if(user == null)
                    {
                        var newuser = await _acctrepo.Addnewuser(newmodel);
                        return Ok(newuser);
                    }
                    return Ok("false");
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
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
                    var user= await _acctrepo.Login(login);
                    return Ok(user);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
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
                    var user= await _sharedrepo.GetuserbyEmail(login.Email);
                    if (user == null)
                        return BadRequest("false");
                    var result= await _acctrepo.UpdatePassword(user,login);
                    return Ok(result);
                }
                return Ok("false");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
