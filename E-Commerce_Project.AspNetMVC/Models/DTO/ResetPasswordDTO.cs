using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.DTO
{
    public class ResetPasswordDTO
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola alanı boş olamaz.")]
        [StringLength(maximumLength: 6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola tekrar alanı boş olamaz.")]
        [Compare("Password", ErrorMessage = "Parola aynı değil.")]
        public string ConfirmPassword { get; set; }
    }
}