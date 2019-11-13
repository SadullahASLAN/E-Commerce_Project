using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class UserAddress : BaseEntity
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        //Mapping
        public virtual User User { get; set; }
    }
}
