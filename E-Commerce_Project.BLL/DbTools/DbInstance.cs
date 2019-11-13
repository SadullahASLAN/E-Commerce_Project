using E_Commerce_Project.DAL.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.DAL.ORM.Tools
{
    public class DbInstance
    {
        private static DataContext _db;

        public static DataContext Instance
        {
            get
            {
                if(_db == null)
                {
                    _db = new DataContext();
                }
                return _db;
            }
        }



    }
}
