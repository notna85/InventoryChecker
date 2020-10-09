using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryChecker.Data.Entities;

namespace InventoryChecker.Interfaces
{
    //This interface serves as a collection of other interfaces
    interface IProductService : IProduct<Product, ProductAmount>, IProductAmount<Product, ProductAmount>, ICategory<Category>, IStorageType<StorageType>
    {
    }
}
