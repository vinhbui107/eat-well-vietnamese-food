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
        public string OrderPhone { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsCompleted { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderDescription { get; set; }
    }
}
