using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DentalWebApi.Models;

namespace DentalWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }

        [HttpGet]
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

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            return Ok();
        }
    }
}