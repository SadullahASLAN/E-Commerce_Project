﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz.")]
        [MaxLength(50, ErrorMessage = "Kullanıcı Adı 50 karakteri geçemez.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "İsim alanı boş olamaz.")]
        [MaxLength(50, ErrorMessage = "İsim alanı 50 karakteri geçemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş olmamaz.")]
        [MaxLength(50, ErrorMessage = "Soyisim alanı 50 karakteri geçemez.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [EmailAddress(ErrorMessage = "Mail adresiniz uygun değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş olamaz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola tekrar alanı boş olamaz.")]
        [Compare("Password", ErrorMessage = "Parola aynı değil.")]
        public string ConfirmPassword { get; set; }
    }
}