using OrderManagment.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagment.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string URL);
        void Insert(T t, string URL);
        void Update(T t, string URL);
        void Delete(int id, string URL);
        T GetById(int id, string URL);
    }
}