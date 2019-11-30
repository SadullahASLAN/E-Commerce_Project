using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Entity
{
    public class MainCategory : BaseEntity
    {
        public MainCategory()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }

        [MaxLength(100, ErrorMessage = "Kategori ismi 100 karakteri geçemez.")]
        [Required(ErrorMessage ="Kategori ismi boş olamaz.")]
        public string Name { get; set; }

        //Mapping       
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
