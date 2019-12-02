using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class UserAddress : BaseEntity
    {
        public string UserId { get; set; }

        [MaxLength(50, ErrorMessage = "Adres başlığı 50 karakteri geçemez.")]
        [Required(ErrorMessage = "Adres başlığı boş olamaz.")]
        public string Title { get; set; }

        [MaxLength(20, ErrorMessage = "Alcı 20 karakteri geçemez.")]
        [Required(ErrorMessage = "Alıcı alanı boş olamaz.")]
        public string ReceivingName { get; set; }

        [Required(ErrorMessage = "Alıcı alanı boş olamaz.")]
        [Phone]
        public string ReceivingPhone { get; set; }

        [MaxLength(300, ErrorMessage = "Adres 300 karakteri geçemez.")]
        [Required(ErrorMessage = "Adres alanı boş olamaz.")]
        public string Address { get; set; }

        //Mapping
        public virtual User User { get; set; }
    }
}
