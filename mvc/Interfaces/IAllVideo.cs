using mvc.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllVideo
    {
        IEnumerable<Video> AllVideo { get; }
    }
}
