using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class Video
    {
        public int Id { set; get; }
        public string Guide { set; get; }
        public int ProductId { set; get; }
        public virtual Product Product { set; get; }
    }
}
