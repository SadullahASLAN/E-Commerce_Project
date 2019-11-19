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
    public class MainCategoryRepository : BaseClass, IBaseRepository<MainCategory>
    {
        public bool AddOrUpdate(MainCategory model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.MainCategories.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(MainCategory model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var mainCategory = db.MainCategories.Find(model.Id);
                if(mainCategory != null)
                {
                    mainCategory.IsDeleted = true;
                    return this.AddOrUpdate(mainCategory);
                }
                else
                {
                    throw new Exception("Seçili kategori bulunamadı.");
                }
            }
        }

        public List<MainCategory> SelectAll()
        {
            return db.MainCategories.Where(i => i.IsDeleted == false).ToList();
        }

        public MainCategory SelectById(string Id)
        {
            return db.MainCategories.Where(i => i.IsDeleted == false && i.Id == Id).FirstOrDefault();
        }

        public MainCategory SelectByName(string name)
        {
            return db.MainCategories.Where(i => i.Name == name && i.IsDeleted == false).FirstOrDefault();
        }
    }
}
