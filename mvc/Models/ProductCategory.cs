using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class ProductCategory
    {
        public IEnumerable<Product> Prod { get; set; }
        public IEnumerable<Category> Cat { get; set; }
    }
    public class OneProductCategory
    {
        public Product Prod { get; set; }
        public IEnumerable<Category> Cat { get; set; }
    }    
}