using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Constants;
using DentalWebApi.Models;
using DentalWebApi.Models.ViewModels;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly DentalContext _dentalContext;

        public RegistrationLogInController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            DentalContext dentalContext
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dentalContext = dentalContext;
        }

        [HttpPost("register")]
        // [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel registerModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Todo Have Register View Model be changed
            var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
            if(userExist != null)
            {
                return BadRequest($"User {registerModel.Email} already exists");
            }

            User newUser = new User()
            {
                //Todo Write user code
            };
            var result = await _userManager.CreateAsync(newUser, registerModel.Password);
            if(result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, Roles.Patient.ToString());
                // return Unauthorized();
                return Ok(newUser);
            }

            var errors = result.Errors;

            // return Ok(registerModel);
            return BadRequest(errors);
        }

        [HttpPost("login")]
        // [ValidateAntiForgeryToken]
        public IActionResult Login([FromBody]LoginViewModel loginModel)
        {
            // SignIn(loginModel);
            return Ok(loginModel);
        }
    }
}