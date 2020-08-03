using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<ProductStorage> StorageType { get; set; }

        public Product(string name, string category, List<ProductStorage> storage)
        {
            Name = name;
            Category = category;
            StorageType = storage;
        }
    }
}
