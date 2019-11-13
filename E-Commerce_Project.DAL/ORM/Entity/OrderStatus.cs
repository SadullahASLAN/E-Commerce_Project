using System;
using System.Collections.Generic;
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

        public string Status { get; set; }
        public string Description { get; set; }

        //Mapping
        public virtual ICollection<Order> Orders { get; set; }
    }
}
