using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce_Project.AspNetMVC.Models.DTO
{
    public class UpdateInformation
    {
        [Required(ErrorMessage = "İsim alanı boş olamaz.")]
        [MaxLength(50, ErrorMessage = "İsim alanı 50 karakteri geçemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş olmamaz.")]
        [MaxLength(50, ErrorMessage = "Soyisim alanı 50 karakteri geçemez.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefon Alanı boş olamaz.")]
        [Phone(ErrorMessage = "Telefon numaranız uygun değil.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email alanı boş olamaz.")]
        [MaxLength(250, ErrorMessage = "Mail alanı 250 karakteri geçemez.")]
        public string Email { get; set; }
    }
}