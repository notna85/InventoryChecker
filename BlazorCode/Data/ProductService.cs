using InventoryChecker.DAL;
using InventoryChecker.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class ProductService
    {
        IDAL pDAL = new ProductDB();
        public string CurrentCategory { get; set; }
        public ProductService()
        {
        }

        public void AddProduct(Product product)
        {
            pDAL.AddProduct(product);
        }
        public Task<List<Product>> GetProductsByCategory()
        {
            return Task.FromResult(pDAL.GetProductsByCategory(CurrentCategory));
        }
        public void UpdateProductAmount(string product, string storagetype, string amount)
        {
            pDAL.UpdateProductAmount(product, storagetype, amount);
        }
        public Task<List<string>> GetCategories()
        {
            return Task.FromResult(pDAL.GetCategories());
        }
        public Task<List<string>> GetStorageTypes()
        {
            return Task.FromResult(pDAL.GetStorageTypes());
        }
    }
}
