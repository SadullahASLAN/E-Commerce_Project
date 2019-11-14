using E_Commerce_Project.BLL.Repositories.Repository;
using E_Commerce_Project.DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mcr = new MainCategoryRepository();
            var mainCategories = mcr.SelectAll();
            foreach(var mainCategory in mainCategories)
            {
                Console.WriteLine($"{mainCategory.Id} | {mainCategory.Name} | {mainCategory.Description}");

                foreach(var subCategory in mainCategory.SubCategories)
                {
                    Console.WriteLine($"{subCategory.Id} | {subCategory.Name} | {subCategory.Description}");
                }
            }










            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Ok");
            Console.ReadLine();
        }
    }
}
