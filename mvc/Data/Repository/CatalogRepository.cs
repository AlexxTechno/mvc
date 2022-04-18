using System;
using Microsoft.EntityFrameworkCore;
using mvc.Interfaces;
using mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class CatalogRepository : IAllCatalog
    {
        private readonly ApplicationDbContext applicationDb;

        public CatalogRepository(ApplicationDbContext applicationDb) 
        {
            this.applicationDb = applicationDb;
        }

        public IEnumerable<Catalog> AllCatalog => (IEnumerable<Catalog>)applicationDb.Category.Include(cat => cat.Product).Where(cat => cat.IsPublished == true).OrderBy(cat => cat.Number);
    }
}
