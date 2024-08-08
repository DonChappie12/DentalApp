using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalWebApi.Models;

namespace DentalWebApi.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> AddAndUpdateUser(User userObj);
    }
}