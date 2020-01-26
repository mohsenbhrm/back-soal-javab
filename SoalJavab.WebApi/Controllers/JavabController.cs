using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DomainClasses;
using SoalJavab.Services;
using SoalJavab.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using SoalJavab.Services.Models;
using Microsoft.AspNetCore.Cors;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class JavabController : ControllerBase
    {
        private IUsersService _user;
        private IJavabServices _javab;
        private ISoalServices _soal;

        public JavabController(IJavabServices javabServices, IUsersService users,ISoalServices soalSevices)
        {
            _user = users;
            _javab = javabServices;
            _soal = soalSevices;
        }

        [HttpGet("[action]")]
        public IActionResult GetbyQuestionId(long Id)
        {
            if (Id < 1 || _soal.isSoalOfuser(Id, _user.GetCurrentUserId()))
            {
                return BadRequest();
            }
            var q = _javab.getJavabofsoalBysoalId(Id);
            if (q != null)
                return Ok(q);
            else
                return NoContent();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var u = _user.GetCurrentUserId();
            if (u > 0)
            {
                var q = _javab.GetAllJavabsByuserId(u);
                if (q != null)
                    return Ok(q);
                return NoContent();
            }
            else
                return BadRequest();

        }
        [HttpGet("{IdSoal}")]
        public IActionResult Get(long IdSoal)
        {
            var u = _user.GetCurrentUserId();
            if (u > 0)
            {
                var q = _javab.getJavabofsoalBysoalId(IdSoal);
                if (q != null)
                    return Ok(q);
                return NoContent();
            }
            else
                return BadRequest();

        }
        
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create([FromBody] JavabVM javab)
        {
            if (!ModelState.IsValid
            || (javab == null))
            {
                return BadRequest();
            }
            var q = _javab.Creatjavab(javab);
            if (q)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public IActionResult Put(long id, [FromBody] JavabVM javab)
        {
            if (
                !ModelState.IsValid
            || !_javab.isJavabOfuser(javab.Id, 1)
            || (javab.Id != id)
            || (javab == null || id < 1))
            {
                return BadRequest();
            }
            var q = _javab.EditeJavab(javab);

            if (q)
            {
                return Ok("*** علی باوامی ***");
            }
            else return BadRequest();
        }
       
        [HttpDelete("{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete(long id)
        {
            var q = _javab.isJavabOfuser(id,_user.GetCurrentUserId());
            if ((id < 1) || q == false)return BadRequest();
            

            if (_javab.Delete(id)) return Ok();

            return StatusCode(500);
        }
    }
}
