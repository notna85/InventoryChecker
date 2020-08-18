using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data.Entities
{
    public class Product
    {
        [Key]
        public string PName { get; set; }
        [Required]
        public string Category { get; set; }

        [ForeignKey("Category")]
        public virtual Category CName { get; set; }

        public Product()
        {
        }
        public Product(string name, string category)
        {
            PName = name;
            Category = category;
        }
    }
}
