using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllImageType
    {
        IEnumerable<ImageType> AllImageType { get; }
    }
}
