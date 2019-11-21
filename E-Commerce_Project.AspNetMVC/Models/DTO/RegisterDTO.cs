using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.DTO
{
    public class RegisterDTO
    {
        private string _userName;
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz.")]
        [MaxLength(15, ErrorMessage = "Kullanıcı Adı 15 karakteri geçemez.")]
        public string UserName { get => _userName; set => _userName = value.Trim(); }

        private string _name;
        [Required(ErrorMessage = "İsim alanı boş olamaz.")]
        [MaxLength(50, ErrorMessage = "İsim alanı 50 karakteri geçemez.")]
        public string Name { get => _name; set => _name = value.Trim(); }

        private string _surName;
        [Required(ErrorMessage = "Soyisim alanı boş olmamaz.")]
        [MaxLength(50, ErrorMessage = "Soyisim alanı 50 karakteri geçemez.")]
        public string Surname { get => _surName; set => _surName = value.Trim(); }

        private string _email;
        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [EmailAddress(ErrorMessage = "Mail adresiniz uygun değil.")]
        public string Email { get => _email; set => _email = value.Trim(); }

        [Required(ErrorMessage = "Parola alanı boş olamaz.")]
        [StringLength(maximumLength: 6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola tekrar alanı boş olamaz.")]
        [Compare("Password", ErrorMessage = "Parola aynı değil.")]
        public string ConfirmPassword { get; set; }
    }
}