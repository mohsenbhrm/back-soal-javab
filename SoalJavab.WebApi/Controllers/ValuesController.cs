using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUsersService _users;
        private IRolesService _role;

        public ValuesController (
         IRolesService IRolesService,
         IUsersService usersService)
        {
            _users = usersService;
            _role = IRolesService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var s = _users.GetCurrentUserId();
            _role.CahngeUserRole(s,new long []{1,2});
             return Ok();
         
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            // var s = await _context.Reshtehs.FindAsync(id);
            // return Ok(s);
            return Ok();
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
