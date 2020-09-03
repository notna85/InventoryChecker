using InventoryChecker.DAL;
using InventoryChecker.Data.Entities;
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
        public void UpdateProduct(string oldName, string newName, List<string> checkedStorages, List<string> uncheckedStorages)
        {
            AddProductAmount(oldName, checkedStorages);
            RemoveProductAmount(oldName, uncheckedStorages);

            if (newName != oldName)
            {
                Product product = dbContext.Product.Find(oldName);
                dbContext.Entry(product).State = EntityState.Detached; //Removes entity from database context so that its primary key values can be updated
                dbContext.Database.ExecuteSqlRaw("Update_Product @p0, @p1", oldName, newName);
            }
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
            dbContext.Entry(productAmount).State = EntityState.Detached; //Removes entity from database context so that an updated one can be fetched
        }
        public void RemoveProduct(string productname)
        {
            Product product = dbContext.Product.Find(productname);
            dbContext.Product.Remove(product);
            dbContext.SaveChanges();
        }
        public void AddProductAmount(string productName, List<string> checkedStorageTypes)
        {
            ProductAmount productAmount;
            foreach (string storage in checkedStorageTypes)
            {
                productAmount = dbContext.ProductAmount.Find(productName, storage);
                if (productAmount == null)
                {
                    dbContext.ProductAmount.Add(new ProductAmount(productName, storage, 0));
                    dbContext.SaveChanges();
                }
            }
        }
        public void RemoveProductAmount(string productName, List<string> uncheckedStorageTypes)
        {
            ProductAmount productAmount;
            foreach (string storage in uncheckedStorageTypes)
            {
                productAmount = dbContext.ProductAmount.Find(productName, storage);
                if (productAmount != null)
                {
                    dbContext.ProductAmount.Remove(productAmount);
                    dbContext.SaveChanges();
                }
            }
        }
        public async Task<List<Category>> GetCategories()
        {
            return await dbContext.Category.ToListAsync();
        }
        public async Task<List<StorageType>> GetStorageTypes()
        {
            return await dbContext.StorageType.ToListAsync();
        }
        public async Task<List<StorageType>> GetStorageTypesByCategory(string category)
        {
            return await dbContext.StorageType.FromSqlRaw("Get_Storages_By_Category @p0", category).ToListAsync();
        }
        public async Task<ProductAmount> GetProductAmount(string oldName, string storage)
        {
            return await dbContext.ProductAmount.FindAsync(oldName, storage);
        }
        public bool ProductExists(string productName)
        {
            if (dbContext.Product.Find(productName) == null)
                return false;
            else
                return true;
        }
        public bool ProductAmountExists(string productName, List<string> storageTypes)
        {
            ProductAmount productAmount;
            foreach (string storage in storageTypes)
            {
                productAmount = dbContext.ProductAmount.Find(productName, storage);
                if (productAmount != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
