using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SoalJavab.Services.Contracts;
using SoalJavab.Services;


namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin")]
    public class manageUsersController : ControllerBase
    {
        private IUsersService _users;

        public manageUsersController(IUsersService users)
        {
            _users = users;
        }
        // make all data for create question page such as list of #Reshteh and #ZirReshteh  
        [HttpGet,Authorize(Roles = "Manager")]
        public IActionResult Get()
        {
            try
            {
                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}