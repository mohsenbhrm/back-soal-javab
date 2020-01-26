using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SoalJavab.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using SoalJavab.Services;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class EssentialsController : ControllerBase
    {
        private IReshtehServices _reshteh;
        private ISoalToUserServices _users;

        public EssentialsController(IReshtehServices reshteh,ISoalToUserServices soalToUserServices)
        {
            _reshteh = reshteh;
            _users = soalToUserServices;
        }
        // make all data for create question page such as list of #Reshteh and #ZirReshteh  
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_reshteh.Get());
            }
            catch { return BadRequest(); }
        }
        [HttpGet("{Id}")]
        public IActionResult Get(long Id)
        {
            try
            {
               
                return Ok();
            }
            catch { return BadRequest("123213"); }
        }
    }
}