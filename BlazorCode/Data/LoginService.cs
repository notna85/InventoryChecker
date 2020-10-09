using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryChecker.DAL;
using Microsoft.EntityFrameworkCore;
using InventoryChecker.Interfaces;

namespace InventoryChecker.Data
{
    public class LoginService : ILogin
    {
        FreezerContext dbContext;
        public LoginService(FreezerContext context) //Constructor sets the value of the database context object
        {
            dbContext = context;
        }
        public bool CheckLogin(string password, string passwordType)
        {
            int result = dbContext.Database.ExecuteSqlRaw("Is_Login_Valid @p0, @p1", password, passwordType);
            if (result == 1)
                return true;
            else
                return false;
        }
    }
}
