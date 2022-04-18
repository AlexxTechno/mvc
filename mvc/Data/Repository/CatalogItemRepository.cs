using Microsoft.EntityFrameworkCore;
using mvc.Interfaces;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class CatalogItemRepository : IAllCatalogItems
    {
        private readonly ApplicationDbContext applicationDb;

        public CatalogItemRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }

        public IEnumerable<CatalogItem> AllCatalogItems => ((IEnumerable<CatalogItem>)applicationDb.Category.Where(cat => cat.IsPublished == true)
                                                                                       .Join(applicationDb.Product.Where(prod => prod.IsPublished == true),
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
                                                                                       })
                                                                                        .Join(applicationDb.Image
                                                                                           .Where(img => img.ImageTypeId == 1),
                                                                                           catalog => catalog.ProductId,
                                                                                           img => img.ProductId,
                                                                                           (catalog, image) => new 
                                                                                       {                                         
                                                                                           PrId = catalog.ProductId,
                                                                                           ImageId = image.Id,
                                                                                           ImageGuide = image.Guide,
                                                                                       })
                                                                                        .Join(applicationDb.Video,
                                                                                           catalog => catalog.PrId,
                                                                                           video => video.ProductId,
                                                                                           (catalog, video) => new
                                                                                       {
                                                                                           VideoGuide = video.Guide,
                                                                                       }))
                                                                                        .OrderBy(item=>item.CategoryNumber)
                                                                                        .ThenBy(item => item.ProductId);   //ProductNumber нужно вместо ProductId но его нет в модели CatalogItem                 
    }               
}
