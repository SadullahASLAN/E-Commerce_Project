﻿using E_Commerce_Project.BLL.Repositories.Interface;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories.Repository
{
    public class ProductRepository : BaseClass, IBaseRepository<Product>
    {
        public bool AddOrUpdate(Product model)
        {
            if(model == null)
            {
                throw new Exception("Eklenen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                db.Products.AddOrUpdate(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(Product model)
        {
            if(model == null)
            {
                throw new Exception("Silinen değer boş. Lütfen tekrar deneyin.");
            }
            else
            {
                var product = db.Products.Find(model.Id);
                if(product != null)
                {
                    product.IsDeleted = true;
                    return this.AddOrUpdate(product);
                }
                else
                {
                    throw new Exception("Seçili ürün bulunamadı.");
                }
            }
        }

        public List<Product> SelectAll()
        {
            return db.Products.Where(i => i.IsDeleted == false).ToList();
        }

        public Product SelectById(string Id)
        {
            return db.Products.Where(i => i.IsDeleted == false && i.Id == Id).First();
        }
    }
}
