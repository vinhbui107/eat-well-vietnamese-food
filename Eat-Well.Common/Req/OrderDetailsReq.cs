using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class OrderDetailsReq
    {
        public int product_id { get; set; }
        public int order_id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }
}
