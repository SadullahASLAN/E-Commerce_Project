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
    class OrderRepository : BaseClass, IBaseRepository<Order>
    {
        public bool AddOrUpdate(Order model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.Orders.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(Order model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var order = db.Orders.Find(model.Id);
                if(order != null)
                {
                    order.IsDeleted = true;
                    return this.AddOrUpdate(order);
                }
                else
                {
                    throw new Exception("Seçili sipariş bulunamadı.");
                }
            }
        }

        public List<Order> SelectAll()
        {
            return db.Orders.Where(i => i.IsDeleted == false).ToList();
        }

        public Order SelectById(string Id)
        {
            return db.Orders.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
