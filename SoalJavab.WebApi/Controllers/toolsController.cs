using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using SoalJavab.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SoalJavab.Services.Models;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class toolsController : ControllerBase
    {
        private IReshteh _reshteh;
        private IZirReshteh _zirReshteh;
        private ITagRepository _tags;

        public toolsController (IReshteh reshteh,IZirReshteh zirReshteh , ITagRepository tagRepository)
        {
            _reshteh = reshteh;
            _zirReshteh = zirReshteh;
            _tags = tagRepository;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetREshtehAsync()
        {
            var q = await _reshteh.GetAsync();
            return Ok(q);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetZirreshteAsync(long id)
        {
            var q = await _zirReshteh.GetAsync(id);
            return Ok(q);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTagsAsync(long ZirReshtehId)
        {
            var q = await _tags.GetByReshtehAsync(ZirReshtehId);
            return Ok(q);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostTagsAsync([FromBody] TagVM t)
        {
            if(t == null)
            { return BadRequest(); }
           
            var q = await _tags.CreatAsync(t);
            return Ok(q);
        }
    }
}