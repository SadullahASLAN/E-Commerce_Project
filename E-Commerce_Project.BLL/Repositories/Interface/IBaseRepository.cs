using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Project.BLL.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        bool AddOrUpdate(T model);
        bool Delete(T model);
        T SelectById(string Id);
        List<T> SelectAll();
    }
}
