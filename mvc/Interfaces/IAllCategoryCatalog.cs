using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllCategoryCatalog
    {
        IEnumerable<CategoryCatalog> IsPublishedCategoryCatalog { get; }
    }
}
