using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetData(string JsonNode);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
