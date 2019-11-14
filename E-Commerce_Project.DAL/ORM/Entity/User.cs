using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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

        //Mapping
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
