using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Models.ViewModels;

namespace DentalWebApi.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginViewModel model);
        public Task<bool> Logout();
        public Task<bool> Register(RegisterViewModel model);
    }
}