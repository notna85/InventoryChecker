using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    interface IProduct<T, U>
    {
        void AddProduct(T product, List<U> paList);
        void UpdateProduct(string oldName, string newName, List<string> checkedStorages, List<string> uncheckedStorages);
        Task<List<T>> GetProductsByCategory(string category);
        void RemoveProduct(T product);
        bool ProductExists(string productName);
    }
}
