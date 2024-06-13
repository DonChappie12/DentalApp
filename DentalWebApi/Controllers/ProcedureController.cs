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
    public class ProcedureController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProcedure()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostProcedure(Procedure procedure)
        {
            return Created();
        }

        [HttpPut]
        public IActionResult PutProcedure(Procedure procedure) { return BadRequest(); }
        
        [HttpDelete]
        public IActionResult DeleteProcedure(Procedure procedure) => Ok();
    }
}