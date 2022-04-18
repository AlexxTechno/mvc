using System;
using System.Collections.Generic;
using mvc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllCatalog
    {
        IEnumerable<Catalog> AllCatalog { get; }
    }
}
