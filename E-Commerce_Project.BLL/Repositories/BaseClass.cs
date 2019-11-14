using E_Commerce_Project.BLL.DbTools;
using E_Commerce_Project.DAL.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories
{
    public class BaseClass
    {
        public DataContext db { get; set; } = DbInstance.Instance;
    }
}
