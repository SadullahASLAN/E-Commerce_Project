using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.Code = Guid.NewGuid().ToString().Substring(0, 5);
            this.InSales = false;
            this.CreatedDate = DateTime.Now;
        }

        [Required(ErrorMessage = "Ürün adı boş geçilemez.")]
        [MaxLength(200, ErrorMessage = "Ürün adı 200 karakteri geçemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ürün kodu boş geçilemez.")]
        [MaxLength(5, ErrorMessage = "Ürün kodu 5 karakteri geçemez.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ürün fiyatı boş geçilemez.")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public short Stock { get; set; }

        [MaxLength(5000, ErrorMessage = "Ürün açıklaması 5000 karakteri geçemez.")]
        [Required(ErrorMessage = "Ürün açıklaması boş olamaz.")]
        public string Description { get; set; }
        public byte? DiscountPercentage { get; set; }

        [MaxLength(100, ErrorMessage = "Marka 100 karakteri geçemez.")]
        public string Brand { get; set; }

        [MaxLength(100, ErrorMessage = "Model 100 karakteri geçemez.")]
        public string Model { get; set; }

        public string SubCategoryId { get; set; }
        public bool InSales { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [MaxLength(300, ErrorMessage = "İndirim açıklaması 300 karakteri geçemez.")]
        public string DiscountDescription { get; set; }

        //Mapping
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
