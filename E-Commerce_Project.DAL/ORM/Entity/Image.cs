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
        [MaxLength(200)]
        [Required]
        public string Paht { get; set; }

        //Mapping
        public virtual MainCategory MainCategory { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Product Product { get; set; }
    }
}
