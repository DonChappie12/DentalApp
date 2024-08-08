using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DentalWebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace DentalWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserbyId(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostUser(User user)
        {
            return Created();
        }

        [HttpPut]
        public IActionResult PutUser(User user)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return Ok();
        }
    }
}