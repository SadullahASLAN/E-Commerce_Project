using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            this.Date = DateTime.Now;
            this.Confirmation = false;
        }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ProductId { get; set; }

        [MaxLength(500, ErrorMessage = "Yorum 500 karakteri geçemez.")]
        public string Explanation { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmation { get; set; }

        [Required(ErrorMessage = "Lütfen ürüne puan veriniz.")]
        public byte? Point { get; set; }

        //Mapping
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}
