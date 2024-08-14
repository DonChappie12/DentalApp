using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalWebApi.Models.Responses
{
    public class LoginResponse
    {
        public bool IsLoggedIn { get; set; } = false;
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}