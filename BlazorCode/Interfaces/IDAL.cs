using InventoryChecker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    public interface IDAL
    {
        public void AddProduct(Product product)
        {
        }
        public List<Product> GetProductsByCategory(string category)
        {
            return new List<Product>();
        }
        public void UpdateProductAmount(string product, string storagetype, string amount)
        {
        }
        public List<string> GetCategories()
        {
            return new List<string>();
        }
        public List<string> GetStorageTypes()
        {
            return new List<string>();
        }
    }
}
