using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class SubCategory : BaseEntity
    {
        public SubCategory()
        {
            this.Products = new HashSet<Product>();
        }

        [MaxLength(100, ErrorMessage = "Kategori ismi 100 karakteri geçemez.")]
        [Required(ErrorMessage = "Kategori ismi boş olamaz.")]
        public string Name { get; set; }

        public string MainCategoryId { get; set; }

        //Mapping
        public virtual ICollection<Product> Products { get; set; }
        public virtual MainCategory MainCategory { get; set; }
    }
}
