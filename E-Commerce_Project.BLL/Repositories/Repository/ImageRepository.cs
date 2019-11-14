using E_Commerce_Project.BLL.Repositories.Interface;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories.Repository
{
    public class ImageRepository : BaseClass, IBaseRepository<Image>
    {
        public bool AddOrUpdate(Image model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.Images.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(Image model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var image = db.Images.Find(model.Id);
                if(image != null)
                {
                    image.IsDeleted = true;
                    return this.AddOrUpdate(image);
                }
                else
                {
                    throw new Exception("Seçili resim bulunamadı.");
                }
            }
        }

        public List<Image> SelectAll()
        {
            return db.Images.Where(i => i.IsDeleted == false).ToList();
        }

        public Image SelectById(string Id)
        {
            return db.Images.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
