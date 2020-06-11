using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class OrdersReq
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderTotal { get; set; }
        public int OrderPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? OrderDescription { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
