using Microsoft.EntityFrameworkCore;
using mvc.Interfaces;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class CategoryRepository : IAllCategory
    {
        private readonly ApplicationDbContext applicationDb;
        // private readonly Category category;

        public CategoryRepository(ApplicationDbContext applicationDb) //, Category category
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<Category> AllCategory => applicationDb.Category.OrderBy(cat => cat.Number);

        public IEnumerable<Category> IsPublishedCategory => applicationDb.Category
            .Include(cat => cat.Product)
            .Where(cat => cat.IsPublished == true)
            .OrderBy(cat => cat.Number);

        /*    public IEnumerable<Category> IsPublishedCatalog => (IEnumerable<Category>)applicationDb.Category.Where(cat => cat.IsPublished == true).OrderBy(cat => cat.Number)
                                                                                           .Join(applicationDb.Product.Where(prod => prod.IsPublished == true).OrderBy(prod => prod.Number),
                                                                                           cat => cat.Id,
                                                                                           prod => prod.CategoryId,
                                                                                           (category, product) => new
                                                                                           {
                                                                                               CategoryId = category.Id,
                                                                                               CategoryNumber = category.Number,
                                                                                               CategoryTitle = category.Title,
                                                                                               CategoryDesc = category.Description,
                                                                                               ProductId = product.Id,
                                                                                               ProductSku = product.Sku,
                                                                                               ProductTitle = product.Title,
                                                                                               ProductIsNew = product.IsNew,
                                                                                               ProductDimension = product.Dimension,
                                                                                               ProductPrice = product.Price,
                                                                                               ProductPriceBig = product.PriceBig,
                                                                                           }).Join(applicationDb.Image.Where(img => img.ImageTypeId == 1),
                                                                                               catalog => catalog.ProductId,
                                                                                               img => img.ProductId,
                                                                                               (catalog, image) => new
                                                                                               {
                                                                                                   PrId = catalog.ProductId,
                                                                                                   ImageId = image.Id,
                                                                                                   ImageGuide = image.Guide,
                                                                                                   ImageType = image.ImageTypeId
                                                                                               }).Join(applicationDb.Video,
                                                                                               catalog => catalog.PrId,
                                                                                               video => video.ProductId,
                                                                                               (catalog, video) => new
                                                                                               {
                                                                                                   VideoGuide = video.Guide,
                                                                                               }); */
    }
}


