using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Product : BaseEntity
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public ushort Stock { get; set; }
        public string Description { get; set; }
        public byte DiscountPercentage { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CategoryId { get; set; }
        public bool InSales { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string DiscountDescription { get; set; }

        //Mapping
        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
