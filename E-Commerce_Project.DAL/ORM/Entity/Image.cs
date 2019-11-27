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
        public string ProductId { get; set; }

        //Mapping
        public virtual Product Product { get; set; }
    }
}
