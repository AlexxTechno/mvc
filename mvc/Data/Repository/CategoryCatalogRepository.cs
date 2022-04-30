using Microsoft.EntityFrameworkCore;
using mvc.Interfaces;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class CategoryCatalogRepository : IAllCategoryCatalog
    {
        private readonly ApplicationDbContext applicationDb;
        // private readonly Category category;

        public CategoryCatalogRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }

        public IEnumerable<CategoryCatalog> IsPublishedCategoryCatalog => (IEnumerable<CategoryCatalog>)applicationDb.Category.Where(cat => cat.IsPublished == true)
                                                                                       .Join(applicationDb.Product.Where(prod => prod.IsPublished == true), // second set
                                                                                       cat => cat.Id,                       // first set selector
                                                                                       prod => prod.CategoryId,             // second set selector
                                                                                       (cat, prod) => new
                                                                                       {
                                                                                           Id = cat.Id,
                                                                                           ProductId = prod.Id,
                                                                                           IsPublished = prod.IsPublished,
                                                                                           Number = prod.Number,
                                                                                           Title = prod.Title,
                                                                                           Sku = prod.Sku,
                                                                                           IsNew = prod.IsNew,
                                                                                           Price = prod.Price,
                                                                                           PriceBig = prod.PriceBig,
                                                                                           Description = prod.Description,
                                                                                           Summary = prod.Summary,
                                                                                           Materials = prod.Materials                                                                                   
                                                                                           })
                                                                                        .Join(applicationDb.Image
                                                                                           .Where(img => img.ImageTypeId == 1),
                                                                                           product => product.ProductId,
                                                                                           img => img.ProductId,
                                                                                           (prod, image) => new
                                                                                           {
                                                                                               ImageId = image.Id,
                                                                                               ProductId = image.ProductId,
                                                                                               Guide = image.Guide,
                                                                                               Quan = image.Quan
                                                                                           })
                                                                                        .Join(applicationDb.Video,
                                                                                           catalog => catalog.ProductId,
                                                                                           video => video.ProductId,
                                                                                           (prod, video) => new
                                                                                           {
                                                                                               VideoId = video.Id,
                                                                                               ProductId = video.ProductId,
                                                                                               Guide = video.Guide
                                                                                           });   //ProductNumber нужно вместо ProductId но его нет в модели CatalogItem  
    }
}
