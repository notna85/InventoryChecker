using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data.Entities
{
    public class StorageType
    {
        [Key]
        public string Stype { get; set; }
    }
}
