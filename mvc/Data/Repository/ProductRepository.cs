using Microsoft.EntityFrameworkCore;
using mvc.Interfaces;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class ProductRepository : IAllProduct
    {
        private readonly ApplicationDbContext applicationDb;

        public ProductRepository(ApplicationDbContext applicationDb)
        {

            this.applicationDb = applicationDb;
        }

        public IEnumerable<Product> AllProduct => applicationDb.Product.Include(prod => prod.Category).Include(img => img.Image).Include(prod => prod.Video);

        public IEnumerable<Product> IsPublishedProduct => applicationDb.Product.Include(prod => prod.Category).Include(img => img.Image).Include(prod => prod.Video).Where(prod => prod.IsPublished == true).Where(prod => prod.Category.IsPublished == true);

        public IEnumerable<Product> IsNewProduct => applicationDb.Product.Where(prod => prod.IsNew).Include(prod => prod.Category).Include(img => img.Image).Include(prod => prod.Video).Where(prod => prod.IsNew == true);

        public Product GetObjectProduct(int productId) => applicationDb.Product.Include(img => img.Image).Include(prod => prod.Category).Include(prod => prod.Video).First(prod => prod.Id == productId);
    }
}