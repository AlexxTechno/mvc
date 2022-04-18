using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models {

    public class Product {
        public int Id { set; get; }
        public bool IsPublished { set; get; }
        public int Number { set; get; }
        public string Title { set; get; }
        public string Sku { set; get; }
        public bool IsNew { set; get; }
        public string Dimension { set; get; }
        public int Price { set; get; }
        public int PriceBig { set; get; }
        public string Description { set; get; }
        public int CategoryId { set; get; }
        public virtual Category Category { set; get; }
        public string Summary { set; get; }
        public string Materials { set; get; }
        public virtual Video Video { set; get; }
        public List<Image> Image { set; get; }
    } 
}

