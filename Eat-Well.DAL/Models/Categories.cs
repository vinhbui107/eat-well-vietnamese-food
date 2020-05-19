using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Categories
    {
        public Categories()
        {
            ProductCategories = new HashSet<ProductCategories>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }

        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
    }
}
