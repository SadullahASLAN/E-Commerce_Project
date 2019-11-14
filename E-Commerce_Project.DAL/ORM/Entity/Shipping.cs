using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Shipping : BaseEntity
    {
        public Shipping()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage ="Kargo firma adı boş geçilemez.")]
        [MaxLength(100,ErrorMessage ="Firma adı 100 karakteri geçemez.")]
        public string Name { get; set; }

        //Mapping
        public virtual ICollection<Order> Orders { get; set; }
    }
}
