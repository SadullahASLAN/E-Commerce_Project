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
    public class ShippingRepository : BaseClass, IBaseRepository<Shipping>
    {
        /// <summary>
        /// Kargo firması ekleme ve güncelleme için kullanılır.
        /// </summary>
        /// <param name="model">Eklenecek veya güncellenecek olan kargo modeli.</param>
        /// <returns>İşlem başarılı olursa geriye true, başarısızsa hata dönderir.</returns>
        /// <exception cref="Başarısız kayıtlarda ve null değer eklemede hata dönderir."></exception>
        public bool AddOrUpdate(Shipping model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.Shippings.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Kargo firması silme işlemleri için kullanılır veritabanından kaldırmaz silinme özelliğine true özelliğini atar.
        /// </summary>
        /// <param name="model">Silinmesini istediğiniz model</param>
        /// <returns>İşlem başarılı olursa geriye true, başarısızsa hata dönderir.</returns>
        /// <exception cref="Başarısız kayıtlarda ve null değer eklemede hata dönderir."></exception>
        public bool Delete(Shipping model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var Shipping = db.Shippings.Find(model.Id);
                if(Shipping != null)
                {
                    Shipping.IsDeleted = true;
                    return this.AddOrUpdate(Shipping);
                }
                else
                {
                    throw new Exception("Seçili kargo firması bulunamadı.");
                }
            }
        }

        /// <summary>
        /// Veritabanında silinme özelliği false olan tüm kargo firmalarını liste halinde getirir.
        /// </summary>
        /// <returns>Liste halinde tüm kargo firmaları (IsDeleted=false)</returns>
        public List<Shipping> SelectAll()
        {
            return db.Shippings.Where(i => i.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Id ye göre veritabanında arama yaparak silinme özelliği false olan kargo firmalarını getirir.
        /// </summary>
        /// <param name="Id">Geriye dondürülecek kargo firmasının id si.</param>
        /// <returns>Kargo firması tipinde model.</returns>
        /// <exception cref="Boş değerde, bulunamayan değerde, hata dönderir."
        public Shipping SelectById(string Id)
        {
            return db.Shippings.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
