using System;
using System.Collections.Generic;

#nullable disable

namespace MVCEntityFramework2Complete.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public byte[] Picture { get; set; }
        public int? Category { get; set; }

        public virtual Category CategoryNavigation { get; set; }
    }
}
