using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Image : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public string ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string CategoryId { get; set; }

        [Key]
        [Column(Order = 3)]
        public string SubCategoryId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Paht { get; set; }

        //Mapping
        public virtual MainCategory MainCategory { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Product Product { get; set; }
    }
}
