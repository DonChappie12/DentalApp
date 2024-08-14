using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DentalWebApi.Constants;
using DentalWebApi.Models;
using DentalWebApi.Models.Responses;
using DentalWebApi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DentalWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if(userExists == null)
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

        public async Task<LoginResponse> Login(LoginViewModel model)
        {
            // ? Maybe have a validation that this user confirms its email before proceeding to login
            // if(!await _userManager.IsEmailConfirmedAsync())
            LoginResponse loginResponse = new LoginResponse();
            var user = await _userManager.FindByNameAsync(model.Alias);

            // TODO Have this the mainpoint of signing in a user to have extra security measures
            // await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return loginResponse;

            // var newRefreshToken = _userManager.GenerateUserTokenAsync(user, "DentalApp", "RefreshToken");

            loginResponse.IsLoggedIn = true;
            loginResponse.JwtToken = await GenerateTokenAsync(user);
            loginResponse.RefreshToken = GenerateRefreshToken();

            user.RefreshToken = loginResponse.RefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(1);
            // await _userManager.UpdateAsync(user);

            return loginResponse;
        }

        public async Task<bool> Logout()
        {
            // var signOutUser = await _signInManager.SignOutAsync();
            return false;
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenViewModel model)
        {
            var principal = GetTokenPrincipal(model.Token);
            var principalName = principal?.Identity?.Name;
            LoginResponse logRes = new LoginResponse();
            if(principalName == null)
                return logRes;
                // return false;

            var user = await _userManager.FindByNameAsync(principalName);

            // * This checks if user is in DB or if refresh toe\kens don't match or the expiry date is longer than current date
            if(user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry > DateTime.UtcNow)
                return logRes;

            logRes.IsLoggedIn = true;
            logRes.JwtToken = await GenerateTokenAsync(user);
            logRes.RefreshToken = GenerateRefreshToken();

            user.RefreshToken = logRes.RefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(1);
            // await _userManager.UpdateAsync(user);

            return logRes;
            // return true;
        }

        private ClaimsPrincipal? GetTokenPrincipal(string jwtToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            string audience = _configuration["JwtSettings:Audience"];
            string issuer = _configuration["JwtSettings:Issuer"];

            var validation = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
            // * The out _ parameter will return the validated token
            return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validation, out _);
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                // ? Change claims to either use JwtRegisteredClaimNames or ClaimTypes
                new Claim(ClaimTypes.Name, user.FirstName),
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

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            // var tokenDescription = new SecurityTokenDescriptor
            // {
            //     Issuer = _configuration["JwtSettings:Issuer"],
            //     Audience  = _configuration["JwtSettings:Audience"],
            //     Expires = DateTime.UtcNow.AddMinutes(1),
            //     SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256),
            //     Subject = new ClaimsIdentity(claims)
            // };

            var signingCred = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature);

            var security = new JwtSecurityToken(
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signingCred
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(security);

            // var tokenHandler = new JwtSecurityTokenHandler();
            // var token = tokenHandler.CreateToken(tokenDescription);
            // return tokenHandler.WriteToken(token);
            return tokenString;

            // return "New Token Generated";
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

    }
}