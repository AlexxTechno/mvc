using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class ImageType
    {
        public int Id { set; get; }
        public string Descripton { set; get; }       
        public List<Image> Image { set; get; }
    }
}
