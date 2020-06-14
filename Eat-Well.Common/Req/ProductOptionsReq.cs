using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class ProductOptionsReq
    {
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
    }
}
