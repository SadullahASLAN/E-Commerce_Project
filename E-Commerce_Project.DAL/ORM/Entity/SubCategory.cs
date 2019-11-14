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
            this.Images = new HashSet<Image>();
        }

        [MaxLength(100, ErrorMessage = "Kategori ismi 100 karakteri geçemez.")]
        [Required(ErrorMessage = "Kategori ismi boş olamaz.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Açıklama 500 karakteri geçemez.")]
        public string Description { get; set; }
        public string MainCategoryId { get; set; }

        //Mapping
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual MainCategory MainCategory { get; set; }
    }
}
