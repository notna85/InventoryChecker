using InventoryChecker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    public interface IDAL<T>
    {
        public void Add(T t)
        {
        }
        public List<T> GetByCategory(string category)
        {
            return new List<T>();
        }
        public void Update(T t, int index, string amount)
        {
        }
        public void Delete(string product)
        {
        }
        public List<string> GetCategories()
        {
            return new List<string>();
        }
        public List<string> GetStorageTypes(string category)
        {
            return new List<string>();
        }
    }
}
