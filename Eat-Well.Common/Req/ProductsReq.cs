using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class ProductsReq
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? ProductSlug { get; set; }
        public bool IsActive { get; set; }

        public List<ProductOptionsReq> Options { get; set; }

    }
}