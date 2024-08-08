using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalWebApi.Constants;
using DentalWebApi.Models;
using DentalWebApi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DentalWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if(userExists != null)
                return false;

            User newUser = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var createdUser = await _userManager.CreateAsync(newUser, model.Password);
            if(!createdUser.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(newUser, Roles.Patient.ToString());
            return true;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            // ? Maybe have a validation that this user confirms its email before proceeding to login
            // if(!await _userManager.IsEmailConfirmedAsync())

            var user = await _userManager.FindByNameAsync(model.Alias);
            // ? maybe refactor this code
            if(user == null)
                return false;
            if(!await _userManager.CheckPasswordAsync(user, model.Password))
                return false;

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                // ? Change claims to either use JwtRegisteredClaimNames or ClaimTypes 
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            // Todo return token
            string token = GenerateToken(authClaims);

            return false;
        }

        public async Task<bool> Logout()
        {
            return false;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience  = _configuration["JwtSettings:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);

            // return "New Token Generated";
        }

    }
}