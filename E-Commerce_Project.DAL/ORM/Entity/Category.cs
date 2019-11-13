using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Category : BaseEntity
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.Images = new HashSet<Image>();
        }

      
        public string Name { get; set; }
        public string Description { get; set; }

        //Mapping
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
