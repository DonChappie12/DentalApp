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
    public class AuthController : ControllerBase

    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        // [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel registerModel)
        {
            // TODO Have the user either confirm email or another way to confirm legitamicy
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
                if(!result.IsLoggedIn)
                    return BadRequest("Your user name or password are invalid");

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
        {
            var result = await _authService.RefreshToken(model);
            if(result.IsLoggedIn)
                return Ok(result);

            return Unauthorized();
        }
    }
}