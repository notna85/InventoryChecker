using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace InventoryChecker.Data.Entities
{
    public class ProductAmount
    {
        [Key]
        [Required]
        public string Product { get; set; }
        [Key]
        [Required]
        public string StorageType { get; set; }
        public int Amount { get; set; }

        [ForeignKey("Product")]
        public virtual Product PName { get; set; }
        [ForeignKey("StorageType")]
        public virtual StorageType SType { get; set; }

        public ProductAmount()
        {
        }
        public ProductAmount(string product, string storage, int amount)
        {
            Product = product;
            StorageType = storage;
            Amount = amount;
        }

    }
}
