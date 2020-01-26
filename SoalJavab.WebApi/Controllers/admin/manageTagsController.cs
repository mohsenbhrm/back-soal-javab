using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SoalJavab.Services.Contracts;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin")]
    public class manageTagsController : ControllerBase
    {
        private ITagServices _tags;

        public manageTagsController(ITagServices tags)
        {
            _tags = tags;
        }
        // make all data for create question page such as list of #Reshteh and #ZirReshteh  
        [HttpGet,Authorize(Roles = "Manager")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_tags.getTags());
            }
            catch { return BadRequest(); }
        }
    }
}