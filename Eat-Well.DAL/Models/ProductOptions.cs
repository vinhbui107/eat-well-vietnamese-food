using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class ProductOptions
    {
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public virtual Options Option { get; set; }
        public virtual Products Product { get; set; }
    }
}
