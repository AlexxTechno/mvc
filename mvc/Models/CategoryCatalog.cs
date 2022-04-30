using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class CategoryCatalog
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Slug { set; get; }        
        public string Description { set; get; }
        public int Number { set; get; }
        public bool IsPublished { set; get; }
        public List<ProductCatalog> ProductCatalog { set; get; }       
    }

    public class ProductCatalog
    {
        public int ProductId { set; get; }
        public bool ProductIsPublished { set; get; }
        public int ProductNumber { set; get; }
        public string ProductTitle { set; get; }
        public string Sku { set; get; }
        public bool IsNew { set; get; }
        public string Dimension { set; get; }
        public int Price { set; get; }
        public int PriceBig { set; get; }
        public string ProductDescription { set; get; }
        public int CategoryId { set; get; }
        public string Summary { set; get; }
        public string Materials { set; get; }
        public List<ImageCatalog> ImageCatalog { set; get; }
        //public virtual VideoCatalog VideoCatalog { set; get; }
    }

    public class ImageCatalog
    {
        public int ImageId { set; get; }
        public string ImageGuide { set; get; }        
        public int Quan { set; get; }
        public int ImageTypeId { set; get; }
        public int ProductId { set; get; }
    }
    /*
    public class VideoCatalog
    {
        public int VideoId { set; get; }
        public string VideoGuide { set; get; }
        public int ProductId { set; get; }
    }
    */
}
