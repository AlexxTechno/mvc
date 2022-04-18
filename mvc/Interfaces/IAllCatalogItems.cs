using System;
using System.Collections.Generic;
using mvc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    interface IAllCatalogItems
    {
        IEnumerable<CatalogItem> AllCatalogItems { get; }
    }
}
