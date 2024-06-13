using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSchedule()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetSchedulebyId(int scheduleId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostSchedule(Schedule schedule)
        {
            return Created();
        }

        [HttpPut]
        public IActionResult PutUser(Schedule schedule)
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