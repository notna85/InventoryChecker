using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    interface ICategory<T>
    {
        Task<List<T>> GetCategories();
    }
}
