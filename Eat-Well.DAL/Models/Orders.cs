using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderTotal { get; set; }
        public string OrderPhone { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsCompleted { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderDescription { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
