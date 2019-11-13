using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
 public   class Order:BaseEntity
    {
        public Order()
        {
            this.OrderStatuses = new HashSet<OrderStatus>();
        }

        public DateTime OrderDate { get; set; }
        public string ShippingId { get; set; }
        public string ShipAddress { get; set; }
        public string TrackingNumber { get; set; }

        //Mapping
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
        public virtual Shipping Shipping { get; set; }

    }
}
