﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryChecker.Data.Entities
{
    public class Category
    {
        [Key]
        public string CName { get; set; }
        public string ImageRef { get; set; }
    }
}
