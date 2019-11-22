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
            this.OrderDetails = new HashSet<OrderDetail>();
            this.OrderDate = DateTime.Now;
            this.TrackingNumber = "Kargoya verilmedi.";
            this.OrderStatus = new OrderStatus() { Status = "Onay Bekleniyor" };
        }

        public string UserId { get; set; }

        public string OrderStatusId { get; set; }

        public DateTime OrderDate { get; set; }

        public string ShippingId { get; set; }

        [MaxLength(500, ErrorMessage = "Sipariş adresi 500 karakteri geçemez.")]
        [Required(ErrorMessage = "Sipariş adresi boş olamaz.")]
        public string UserAddressId { get; set; }

        [MaxLength(50, ErrorMessage = "Takip numarası 50 karakteri geçemez.")]
        public string TrackingNumber { get; set; }

        //Mapping
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual User User { get; set; }
        public UserAddress UserAddress { get; set; }
    }
}
