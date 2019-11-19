using E_Commerce_Project.BLL.Repositories.Interface;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories.Repository
{
    public class CommentRepository : BaseClass, IBaseRepository<Comment>
    {
        public bool AddOrUpdate(Comment model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.Comments.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(Comment model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var comment = db.Comments.Find(model.Id);
                if(comment != null)
                {
                    comment.IsDeleted = true;
                    return this.AddOrUpdate(comment);
                }
                else
                {
                    throw new Exception("Seçili yorum bulunamadı.");
                }
            }
        }

        public List<Comment> SelectAll()
        {
            return db.Comments.Where(i => i.IsDeleted == false).ToList();
        }

        public Comment SelectById(string Id)
        {
            return db.Comments.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
