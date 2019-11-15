using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [EmailAddress(ErrorMessage = "Mail adresiniz uygun değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş olamaz.")]
        public string Password { get; set; }
    }
}