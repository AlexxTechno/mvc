using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Image
    {
        public int Id { set; get; }
        public string Guide { set; get; }
        public int ProductId { set; get; }
        public int Quan { set; get; }
        public virtual Product Product { set; get; }
        public int ImageTypeId { set; get; }
        public virtual ImageType ImageType { set; get; }
    }
}
