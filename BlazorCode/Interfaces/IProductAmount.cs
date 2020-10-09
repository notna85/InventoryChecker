using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    interface IProductAmount<T, U>
    {
        Task<U> GetProductAmount(string oldName, string storage);
        Task<List<U>> GetProductAmountsByProduct(T product);
        void UpdateProductAmount(U productAmount, int amount);
        void PrepareAddProductAmount(string productName, List<string> checkedStorageTypes);
        void PrepareRemoveProductAmount(string productName, List<string> uncheckedStorageTypes);
        void AddProductAmount(U productAmount);
        void RemoveProductAmount(U productAmount);
        bool ProductAmountExists(string productName, List<string> storageTypes);
    }
}
