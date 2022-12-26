using Ecommerce_api.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_api.Controllers
{
    [ApiController]
    [Route("api/Shared")]
    [EnableCors("SiteCorsPolicy")]
    public class SharedController : Controller
    {
        private readonly ISharedRepo _sharedrepo;
        public SharedController(ISharedRepo sharedrepo)
        {
            _sharedrepo = sharedrepo;
        }

        [HttpGet]
        [Route("countryCode")]
        public async Task<IActionResult> getallcountrycodes()
        {
            try
            {
                var result = await _sharedrepo.getallcountrycodes();
                return Ok(result==null? "No data available":result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("getuserbyEmail/{email}")]
        public async Task<IActionResult> GetuserbyEmail(string email)
        {
            try
            {
                if (email == null)
                    return BadRequest("enter valid input");
                var user = await _sharedrepo.GetuserbyEmail(email);
                if (user != null)
                {
                    return Ok(user);
                }
                return Problem("Email doesn't Exist");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("getallUsers")]
        public async Task<IActionResult> GetAllusers()
        {
            try
            {
                var user = await _sharedrepo.Getallusers();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
