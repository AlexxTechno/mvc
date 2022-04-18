using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllProduct
    {
        IEnumerable<Product> AllProduct { get;  }   // set;

        IEnumerable<Product> IsPublishedProduct { get; }   // set;

        IEnumerable<Product> IsNewProduct { get; }  // set;

        Product getObjectProduct(int productId);

    }
}
