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
    public class OrderStatusRepository : BaseClass, IBaseRepository<OrderStatus>
    {
        /// <summary>
        /// Sipariş durumu ekleme ve güncelleme için kullanılır.
        /// </summary>
        /// <param name="model">Eklenecek veya güncellenecek olan sipariş durum modeli.</param>
        /// <returns>İşlem başarılı olursa geriye true, başarısızsa hata dönderir.</returns>
        /// <exception cref="Başarısız kayıtlarda ve null değer eklemede hata dönderir."></exception>
        public bool AddOrUpdate(OrderStatus model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.OrderStatuses.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Sipariş durum silme işlemleri için kullanılır veritabanından kaldırmaz, silinme özelliğine true özelliğini atar.
        /// </summary>
        /// <param name="model">Silinmesini istediğiniz model</param>
        /// <returns>İşlem başarılı olursa geriye true, başarısızsa hata dönderir.</returns>
        /// <exception cref="Başarısız kayıtlarda ve null değer eklemede hata dönderir."></exception>
        public bool Delete(OrderStatus model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var OrderStatus = db.OrderStatuses.Find(model.Id);
                if(OrderStatus != null)
                {
                    OrderStatus.IsDeleted = true;
                    return this.AddOrUpdate(OrderStatus);
                }
                else
                {
                    throw new Exception("Seçili sipariş durum bulunamadı.");
                }
            }
        }

        /// <summary>
        /// Veritabanında silinme özelliği false olan tüm sipariş durumlarını liste halinde getirir.
        /// </summary>
        /// <returns>Liste halinde tüm sipariş durumları (IsDeleted=false)</returns>
        public List<OrderStatus> SelectAll()
        {
            return db.OrderStatuses.Where(i => i.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Id ye göre veritabanında arama yaparak silinme özelliği false olan sipariş durumlarını getirir.
        /// </summary>
        /// <param name="Id">Geriye dondürülecek sipariş durumunun id si.</param>
        /// <returns>Sipariş durum tipinde model.</returns>
        /// <exception cref="Boş değerde, bulunamayan değerde, hata dönderir."
        public OrderStatus SelectById(string Id)
        {
            return db.OrderStatuses.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
