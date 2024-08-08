using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalWebApi.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        // [DisplayName]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        // [Display(Name = "")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}