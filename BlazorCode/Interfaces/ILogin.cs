using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Interfaces
{
    interface ILogin
    {
        bool CheckLogin(string password, string passwordType);
    }
}
