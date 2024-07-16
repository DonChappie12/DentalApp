using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DentalContext _dentalContext;

        public RegistrationLogInController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
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
            // var userExist = await _userManager.FindByEmailAsync(registerModel.EmailAddress);
            // if(userExist != null)
            // {
            //     return BadRequest($"User {registerModel.EmailAddress} already exists");
            // }

            // User newUser = new User()
            // {
            //     //* Write user code
            // };
            // var result = await _userManager.CreateAsync(newUser, registerModel.Password);
            // if(result.Succeeded) 
            // {
            //     await _userManager.AddToRoleAsync(newUser, "Customer");
            //     // return Unauthorized();
            //     return Ok(newUser);
            // }

            return Ok();
            // return BadRequest("User could not be created");
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