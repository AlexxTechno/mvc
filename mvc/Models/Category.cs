using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models { 

     public class Category {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Slug { set; get; }
        public List<Product> Product { set; get; }
       // public List<Image> Image { set; get; }
        public string Description { set; get; }
        public int Number { set; get; }
        public bool IsPublished { set; get; }
    }
}