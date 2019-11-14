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
    public class OrderDetailRepository : BaseClass, IBaseRepository<OrderDetail>
    {
        public bool AddOrUpdate(OrderDetail model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.orderDetails.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(OrderDetail model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var orderDetail = db.orderDetails.Find(model.Id);
                if(orderDetail != null)
                {
                    orderDetail.IsDeleted = true;
                    return this.AddOrUpdate(orderDetail);
                }
                else
                {
                    throw new Exception("Seçili sipariş detayı bulunamadı.");
                }
            }
        }

        public List<OrderDetail> SelectAll()
        {
            return db.orderDetails.Where(i => i.IsDeleted == false).ToList();
        }

        public OrderDetail SelectById(string Id)
        {
            return db.orderDetails.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
