using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    interface IStorageType<T>
    {
        Task<List<T>> GetStorageTypes();
        Task<List<T>> GetStorageTypesByCategory(string category);
    }
}
