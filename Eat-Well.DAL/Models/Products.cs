using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ProductOptions = new HashSet<ProductOptions>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Photo { get; set; }
        public string Desciption { get; set; }
        public string ProductSlug { get; set; }
        public bool IsActive { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<ProductOptions> ProductOptions { get; set; }
    }
}
