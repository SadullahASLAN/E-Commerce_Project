using E_Commerce_Project.BLL.Repositories.Interface;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace E_Commerce_Project.BLL.Repositories.Repository
{
    public class UserAddressRepository : BaseClass, IBaseRepository<UserAddress>
    {
        public bool AddOrUpdate(UserAddress model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.UserAddresses.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(UserAddress model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var userAddress = db.UserAddresses.Find(model.Id);
                if(userAddress != null)
                {
                    userAddress.IsDeleted = true;
                    return this.AddOrUpdate(userAddress);
                }
                else
                {
                    throw new Exception("Seçili adres bulunamadı.");
                }
            }
        }

        public List<UserAddress> SelectAll()
        {
            return db.UserAddresses.Where(i => i.IsDeleted == false).ToList();
        }

        public UserAddress SelectById(string Id)
        {
            return db.UserAddresses.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
