using InventoryChecker.DAL;
using InventoryChecker.Data.Entities;
using InventoryChecker.Interfaces;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryChecker.Data
{
    public class ProductService
    {
        FreezerContext dbContext;
        public static string CurrentCategory { get; set; }

        public ProductService(FreezerContext context)
        {
            dbContext = context;
        }
        public void AddProduct(ProductModel productModel)
        {
            dbContext.Product.Add(productModel.Product);
            foreach(ProductAmount pa in productModel.ProductAmountList)
            {
                dbContext.ProductAmount.Add(pa);
            }
            dbContext.SaveChanges();
        }
        public async Task<List<ProductModel>> GetProductsByCategory()
        {
            List<ProductModel> pmList = new List<ProductModel>();
            List<Product> pList = await dbContext.Product.Where(p => p.Category == CurrentCategory).ToListAsync();
            foreach(Product p in pList)
            {
                List<ProductAmount> paList = dbContext.ProductAmount.Where(pa => pa.Product == p.PName).ToList();
                ProductModel pm = new ProductModel(p, paList);
                pmList.Add(pm);
            }
            return pmList; 
        }
        public void UpdateProductAmount(ProductAmount productAmount, int amount)
        {
            dbContext.Database.ExecuteSqlRaw("Update_Amount @p0, @p1, @p2", amount, productAmount.Product, productAmount.StorageType);
            dbContext.Entry(productAmount).State = EntityState.Detached;
        }
        public void RemoveProduct(string productname)
        {
            Product product = dbContext.Product.Find(productname);
            dbContext.Product.Remove(product);
            dbContext.SaveChanges();
        }
        public async Task<List<Category>> GetCategories()
        {
            return await dbContext.Category.ToListAsync();
        }
        public async Task<List<StorageType>> GetStorageTypes()
        {
            return await dbContext.StorageType.ToListAsync();
        }
    }
}
