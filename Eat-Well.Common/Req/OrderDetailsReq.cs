using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class OrderDetailsReq
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
