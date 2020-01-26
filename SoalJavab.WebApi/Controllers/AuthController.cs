using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoalJavab.DataLayer;
using SoalJavab.DomainClasses;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using SoalJavab.Services.Models;

namespace SoalJavab.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       public AuthController()
        { }

        // POST api/values
        // [HttpPost, Route("login")]
        // [EnableCors("EnableCORS")]
        // public IActionResult Lognin([FromBody] LoginModel user)
        // {
        //     if (user == null)
        //     {
        //         return BadRequest("invalid client request");
        //     }
        //     var keybytes = Encoding.ASCII.GetBytes("adsdasda-sadadsa");
        //     var singinkey1 = new SymmetricSecurityKey(keybytes);
        //     if (user.UserName == "mohsen" && user.Password == "123456")
        //     {
        //         var secretKey = singinkey1;// new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SupersecretKey@1234"));
        //         var singincredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name,user.UserName),
        //             new Claim(ClaimTypes.Role,"Manager")
        //         };
        //         var tokenOption = new JwtSecurityToken(
        //             issuer: "http://localhost:5000",
        //             audience: "http://localhost:5000",
        //             claims: claims,
        //             expires: DateTime.Now.AddMinutes(5),
        //             signingCredentials: singincredentials
        //         );
        //         var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
        //         return Ok(new { Token = tokenString });
        //     }
        //     else
        //     {
        //         return Unauthorized();
        //     }
        // }

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
