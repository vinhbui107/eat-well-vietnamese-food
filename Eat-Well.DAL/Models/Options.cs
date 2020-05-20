using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Options
    {
        public Options()
        {
            ProductOptions = new HashSet<ProductOptions>();
        }

        public int OptionId { get; set; }
        public string OptionName { get; set; }

        public virtual ICollection<ProductOptions> ProductOptions { get; set; }
    }
}
