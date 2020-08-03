using InventoryChecker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class ProductService
    {
        public string ConnectionString { get; set; }
        public ProductService(string connectionstring)
        {
            ConnectionString = connectionstring;
        }
        //ProductDAL pDAL = new ProductDAL(ConnectionString);

        public void AddProduct(Product product)
        {
            pDAL.AddProduct(product);
        }
        public List<Product> GetProducts(string category)
        {
            return new List<Product>();
        }
        public void UpdateProductAmount()
        {
            pDAL.UpdateProductAmount();
        }
        public List<string> GetCategories()
        {
            return new List<string>();
        }
    }
}
