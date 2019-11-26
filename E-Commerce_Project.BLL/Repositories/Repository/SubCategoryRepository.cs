using E_Commerce_Project.BLL.DbTools;
using E_Commerce_Project.BLL.Repositories.Interface;
using E_Commerce_Project.DAL.ORM.Context;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories.Repository
{
    public class SubCategoryRepository : BaseClass, IBaseRepository<SubCategory>
    {
        public bool AddOrUpdate(SubCategory model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.SubCategories.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(SubCategory model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var subCategory = db.SubCategories.Find(model.Id);
                if(subCategory != null)
                {
                    subCategory.IsDeleted = true;
                    return this.AddOrUpdate(subCategory);
                }
                else
                {
                    throw new Exception("Seçili kategori bulunamadı.");
                }
            }
        }

        public List<SubCategory> SelectAll()
        {
            return db.SubCategories.Where(i => i.IsDeleted == false).ToList();
        }

        public SubCategory SelectById(string Id)
        {
            return db.SubCategories.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }

        public List<SubCategory> MainCategoryId(string id)
        {
            if(id == null) return null;

            var main = db.MainCategories.FirstOrDefault(i => i.Id == id && i.IsDeleted == false);
            if(main == null) return null;

            return main.SubCategories.ToList();
        }
    }
}
