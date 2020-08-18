using InventoryChecker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<ProductAmount> ProductAmountList { get; set; }

        public ProductModel(Product product, List<ProductAmount> paList)
        {
            Product = product;
            ProductAmountList = paList;
        }
    }
}
