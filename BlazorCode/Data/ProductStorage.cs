using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class ProductStorage
    {
        public string Type { get; set; }
        public int Amount { get; set; }

        public ProductStorage(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
