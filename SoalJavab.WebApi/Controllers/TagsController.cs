using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SoalJavab.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private ITagServices _tags;
        private IUsersService _user;

        public TagsController(ITagServices itag, IUsersService users)
        {
            _user = users;
            _tags = itag;
        }

        [HttpGet("{id}/{TagName}")]
        public IActionResult Get(long id, string TagName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var q = _tags.GetTags(id, TagName);

                    if (q != null) return Ok(q);

                    else return NoContent();
                }
                else
                {
                    return BadRequest(id + TagName);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok("بدون پیاده سازی");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
