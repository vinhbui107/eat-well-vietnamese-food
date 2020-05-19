using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ProductCategories = new HashSet<ProductCategories>();
            ProductOptions = new HashSet<ProductOptions>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string ProductSlug { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
        public virtual ICollection<ProductOptions> ProductOptions { get; set; }
    }
}
