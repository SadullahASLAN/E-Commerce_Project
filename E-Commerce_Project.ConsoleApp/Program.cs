using E_Commerce_Project.DAL.ORM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Project.DAL.ORM.Entity;

namespace E_Commerce_Project.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Product = DbInstance.Instance.Products.ToList();

            Console.WriteLine("Veritabanı oluştu");
            Console.ReadLine();
        }
    }
}
