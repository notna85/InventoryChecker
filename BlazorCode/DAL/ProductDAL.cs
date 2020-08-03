using InventoryChecker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.DAL
{
    public class ProductDAL
    {
        public string ConnectionString { get; set; }
        public ProductDAL(string connection)
        {
            ConnectionString = connection;
        }
        public void AddProduct(Product product)
        {

        }
        public List<Product> GetProducts(string category)
        {
            return new List<Product>();
        }
        public void UpdateProductAmount()
        {

        }
        public List<string> GetCategories()
        {
            return new List<string>();
        }
    }
}
