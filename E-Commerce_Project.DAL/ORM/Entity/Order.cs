using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OrderStatuses = new HashSet<OrderStatus>();
            this.OrderDate = DateTime.Now;
            this.TrackingNumber = "Kargoya verilmedi.";
        }

        [Required(ErrorMessage = "Musteri alanı boş olamaz.")]
        public string UsurId { get; set; }

        public DateTime OrderDate { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Kargo firması boş geçilemez.")]
        public string ShippingId { get; set; }

        [MaxLength(500, ErrorMessage = "Sipariş adresi 500 karakteri geçemez.")]
        [Required(ErrorMessage = "Sipariş adresi boş olamaz.")]
        public string ShipAddress { get; set; }

        [MaxLength(50, ErrorMessage = "Takip numarası 50 karakteri geçemez.")]
        public string TrackingNumber { get; set; }

        //Mapping
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }

        public virtual Shipping Shipping { get; set; }

        public virtual User User { get; set; }

    }
}
