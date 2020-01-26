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
    public class SoalToUserController : ControllerBase
    {
        private ISoalToUserServices _soalToUser;
        private ISoalServices _soal;
        private IUsersService _user;

        public SoalToUserController(ISoalToUserServices soalToUserSevices,ISoalServices soal, IUsersService usersService)
        {
            _soalToUser = soalToUserSevices;
            _soal = soal;
            _user = usersService;
        }
        //GET: api/SoalToUser

        [HttpGet]
        public IActionResult Get()
        {
            var q = _soalToUser.GetnewSoalTouserByIdUser(_user.GetCurrentUserId());
            if (q==null)
            {return NoContent();}
            
            return Ok(q.Take(10));
        }
        [HttpGet("[action]")]
        public IActionResult GetNewfeeds()
        {
            var q = _soalToUser.GetnewFeedSoalTouserByIdUser(_user.GetCurrentUserId());
            return Ok(q);
        }

        // GET: api/Soal/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetSoalToUserById(long id)
        {
            var q = _soalToUser.getSoalToUserByUserID(id,_user.GetCurrentUserId());
            return Ok(q);
        }
        [HttpPost]
        public IActionResult Create([FromBody] SoalVM soal)
        {
            // if (!ModelState.IsValid
            // || (soal == null))
            // {
            //     return BadRequest("**  شیطون بلا  **");
            // }
            // var q = _soal.postforSoal(soal, 5);
            // if (q)
            // {
            //     return Ok(new JsonResult("*** علی باوامی ***"));
            // }
            return StatusCode(500);
        }
        [HttpPut]
        public IActionResult Put(long id, [FromBody] SoalEditVM soal)
        {
            // if (
            //     !ModelState.IsValid
            // || !_soal.isSoalOfuser(soal.Id, 5)
            // || (soal.Id != id)
            // || (soal == null || id < 1))
            // {
            //     return BadRequest("**  شیطون بلا  **");
            // }
            // var q = _soal.EditforSoal(soal);

            // if (q)
            // {
            //     return Ok("*** علی باوامی ***");
            // }


            return StatusCode(500);
        }
        [HttpDelete]
        public IActionResult delete(long Id)
        {
            // if (Id < 1) return BadRequest();

            // if (_soalToUser.DeleteSoal(Id)) return Ok();

            return StatusCode(500);

        }
    }
}
