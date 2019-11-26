using GroceryStoreAPI.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public GenericRepository()
        {

        }

        public IEnumerable<T> GetData(string JsonNode)
        {
            using StreamReader r = new StreamReader("database.json");
            string json = r.ReadToEnd();
            JObject data = JObject.Parse(json);
            JArray JSONItems = (JArray)data[JsonNode];
            List<T> items = JsonConvert.DeserializeObject<List<T>>(JSONItems.ToString());

            return items;
        }

        public void Insert(T obj)
        {
            //Save to database logic here;
        }

        public void Update(T obj)
        {
            //Update to database logic here
        }

        public void Delete(object id)
        {
            //Delete from database logic here
        }
    }
}
