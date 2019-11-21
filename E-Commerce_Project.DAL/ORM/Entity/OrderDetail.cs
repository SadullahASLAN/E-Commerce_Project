using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class OrderDetail : BaseEntity
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Urun adeti boş geçilemez.")]
        public short Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        //Mapping
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
