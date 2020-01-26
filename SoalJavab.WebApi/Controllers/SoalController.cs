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
    public class SoalController : ControllerBase
    {
        private ISoalServices _soal;
        private IUsersService _users;
        public SoalController(ISoalServices soalSevices,IUsersService usersService)
        {
            _soal = soalSevices;
            _users = usersService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var user = _users.GetCurrentUserId();
            var q = _soal.getSoalbyIdUser(user);
            if (q != null)
                return Ok(q);
            else
                return Ok("چیزی نیست");
        }
        [HttpGet("{SoalId}")]
        public IActionResult Get(long SoalId)
        {
            if (!ModelState.IsValid || SoalId<1)
            {return BadRequest();}
            var q = _soal.getSoalbyId(SoalId);
            if (q != null)
                return Ok(q);
            else
                return Ok("چیزی نیست");
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create([FromBody] SoalVM soal)
        {
            if (!ModelState.IsValid
            || (soal == null))
            {
                return BadRequest("**  شیطون بلا  **");
            }
            var q = _soal.postforSoal(soal, _users.GetCurrentUserId());
            if (q)
            {
                return Ok(new JsonResult("*** علی باوامی ***"));
            }
            return StatusCode(500);
        }
        [HttpPut("{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult Put(long id, [FromBody] SoalEditVM soal)
        {
            var us = _users.GetCurrentUserId();
            if (
                !ModelState.IsValid
            || !_soal.isSoalOfuser(soal.Id, us)
            || (soal.Id != id)
            || (soal == null || id < 1))
            {
                return BadRequest();
            }
            var q = _soal.EditforSoal(soal);

            if (q)
            {
                return Ok();
            }


            return BadRequest();
        }

        [HttpDelete("{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete(long id)
        {

            if (id < 1 || !_soal.isSoalOfuser(id, _users.GetCurrentUserId())) return BadRequest();

            if (_soal.DeleteSoal(id)) return Ok();

            return StatusCode(500);

        }
    }
}
