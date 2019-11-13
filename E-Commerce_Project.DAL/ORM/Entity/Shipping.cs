using System;
using System.Collections.Generic;
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

        public string Name { get; set; }

        //Mapping
        public virtual ICollection<Order> Orders { get; set; }
    }
}
