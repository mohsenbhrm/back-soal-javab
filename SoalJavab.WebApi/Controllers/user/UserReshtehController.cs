using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services;
using SoalJavab.Services.Contracts;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class UserReshtehController : ControllerBase
    {
        private IUsersService _user;
        private IReshtehServices _reshteh;

        public UserReshtehController (IUsersService usersService,IReshtehServices reshtehServices)
        {
            _user = usersService;
            _reshteh = reshtehServices;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            _user.GetCurrentUserId();
            // var q = _reshteh.
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] string value)
        {

        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }
    }
}
