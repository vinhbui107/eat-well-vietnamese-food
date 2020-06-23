using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class OrdersReq
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int order_total { get; set; }
        public string phone { get; set; }
        public DateTime? date { get; set; }
        public bool is_completed { get; set; }
        public string shipping_address { get; set; }
        public string description { get; set; }
    }
}
