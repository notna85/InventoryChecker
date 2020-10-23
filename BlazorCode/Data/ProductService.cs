using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InventoryChecker.DAL;
using InventoryChecker.Data.Entities;
using InventoryChecker.Interfaces;

namespace InventoryChecker.Data
{
    //Service class that handles database related operations
    public class ProductService : IProductService
    {
        FreezerContext dbContext;
        public ProductService(FreezerContext context)
        {
            dbContext = context;
        }

        public void AddProduct(Product product, List<ProductAmount> paList)
        {
            dbContext.Product.Add(product);
            foreach(ProductAmount pa in paList)
            {
                dbContext.ProductAmount.Add(pa);
            }
            dbContext.SaveChanges();
        }
        public void UpdateProduct(string oldName, string newName, List<string> checkedStorages, List<string> uncheckedStorages)
        {
            PrepareAddProductAmount(oldName, checkedStorages);
            PrepareRemoveProductAmount(oldName, uncheckedStorages);

            if (newName != oldName)
            {
                Product product = dbContext.Product.Find(oldName);
                dbContext.Entry(product).State = EntityState.Detached; //Removes entity from database context so that its primary key values can be updated
                dbContext.Database.ExecuteSqlRaw("Update_Product @p0, @p1", oldName, newName);
            }
        }
        public async Task<List<Product>> GetProductsByCategory(string category)
        {
            return await dbContext.Product.Where(p => p.Category == category).ToListAsync();
        }
        public void UpdateProductAmount(ProductAmount productAmount, int amount)
        {
            dbContext.Database.ExecuteSqlRaw("Update_Amount @p0, @p1, @p2", amount, productAmount.Product, productAmount.StorageType);
        }
        public void RemoveProduct(Product product)
        {
            dbContext.Product.Remove(product);
            dbContext.SaveChanges();
        }
        public void PrepareAddProductAmount(string productName, List<string> checkedStorageTypes)
        {
            foreach (string storage in checkedStorageTypes)
            {
                if (dbContext.ProductAmount.Find(productName, storage) == null)
                {
                    AddProductAmount(new ProductAmount(productName, storage, 0));
                }
            }
        }
        public void PrepareRemoveProductAmount(string productName, List<string> uncheckedStorageTypes)
        {
            ProductAmount productAmount;
            foreach (string storage in uncheckedStorageTypes)
            {
                productAmount = dbContext.ProductAmount.Find(productName, storage);
                if (productAmount != null)
                {
                    RemoveProductAmount(productAmount);
                }
            }
        }
        public void AddProductAmount(ProductAmount productAmount)
        {
            dbContext.ProductAmount.Add(productAmount);
            dbContext.SaveChanges();
        }
        public void RemoveProductAmount(ProductAmount productAmount)
        {
            dbContext.ProductAmount.Remove(productAmount);
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
        public async Task<List<StorageType>> GetStorageTypesByCategory(string category)
        {
            return await dbContext.StorageType.FromSqlRaw("Get_Storages_By_Category @p0", category).ToListAsync();
        }
        public async Task<List<ProductAmount>> GetProductAmountsByProduct(Product product)
        {
            List<ProductAmount> paList = dbContext.ProductAmount.Where(pa => pa.Product == product.PName).ToList();
            foreach(ProductAmount pa in paList)
            {
                dbContext.Entry(pa).State = EntityState.Detached;
            }
            return await dbContext.ProductAmount.Where(pa => pa.Product == product.PName).ToListAsync();
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
            foreach (string storage in storageTypes)
            {
                if (dbContext.ProductAmount.Find(productName, storage) != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
