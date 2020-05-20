using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class OrderDetails
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
