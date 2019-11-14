using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class OrderStatus : BaseEntity
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "Durum boş olamaz.")]
        [MaxLength(50, ErrorMessage = "Durum 50 karakteri geçemez.")]
        public string Status { get; set; }

        [MaxLength(300, ErrorMessage ="Durum açıklaması 300 karakteri geçemez.")]
        public string Description { get; set; }

        //Mapping
        public virtual ICollection<Order> Orders { get; set; }
    }
}
