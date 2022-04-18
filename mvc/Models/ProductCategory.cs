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

    public class Catalog
    {
        public Category Cat { get; set; }
        public IEnumerable<Product> Prod { get; set; }
    }
    
    public class CatalogItem
    {
        /*
        public Category Cat { get; set; }
        public IEnumerable<Product> Prod { get; set; }
        public IEnumerable<Image> Img { get; set; }
        public IEnumerable<Video> Video { get; set; }      
        */       

        public int CategoryId;
        public int CategoryNumber;
        public string CategoryTitle;
        public string CategoryDesc;

        public int ProductId;
        public string ProductSku;
        public string ProductTitle;
        public bool ProductIsNew;
        public string ProductDimension;
        public int ProductPrice;
        public int ProductPriceBig;
        public int PrId;

        public int ImageId;
        public string ImageGuide;

        public string VideoGuide;

        internal object AllCatalogItems;
    }
}
