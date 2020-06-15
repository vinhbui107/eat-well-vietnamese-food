using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class ProductsReq
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string name { get; set; }
        public string? photo { get; set; }
        public string? description { get; set; }
        public string? slug { get; set; }
        public bool is_active { get; set; }

        public List<ProductOptionsReq> Options { get; set; }

    }
}