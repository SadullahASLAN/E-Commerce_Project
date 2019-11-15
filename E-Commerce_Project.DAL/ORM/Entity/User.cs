using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UserAddresses = new HashSet<UserAddress>();
            this.Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "İsim alanı boş olamaz.")]
        [MaxLength(50, ErrorMessage = "İsim alanı 50 karakteri geçemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş olmamaz.")]
        [MaxLength(50, ErrorMessage = "Soyisim alanı 50 karakteri geçemez.")]
        public string Surname { get; set; }

        //Mapping
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
