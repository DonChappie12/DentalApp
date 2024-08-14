using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalWebApi.Models.ViewModels
{
    public class RefreshTokenViewModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}