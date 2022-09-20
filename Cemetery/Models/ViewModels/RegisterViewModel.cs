using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Jelszó")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} legyen minimum {2} karakter hosszú.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó megerősítése")]
        [Compare("Password", ErrorMessage = "A jelszó és a megerősített jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Szerepkör megnevezése")]
        public string RoleName { get; set; }
    }
}
