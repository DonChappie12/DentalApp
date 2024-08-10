using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Constants;
using DentalWebApi.Models;
using DentalWebApi.Models.ViewModels;
using DentalWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalWebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationLogInController : ControllerBase

    {
        private readonly IAuthService _authService;

        public RegistrationLogInController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        // [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel registerModel)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();

                var result = await _authService.Register(registerModel);
                if(!result)
                    return BadRequest();

                return Ok(registerModel);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginModel)
        {
            // SignIn(loginModel);
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();

                var result = await _authService.Login(loginModel);
                if(!result)
                    return BadRequest();
                // Todo have token be sent to the front end client
                return Ok(loginModel);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}