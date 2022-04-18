using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Interfaces;
using mvc.Models;

namespace mvc.Data.Repository
{
    public class ImageRepository : IAllImage
    {
        private readonly ApplicationDbContext applicationDb;

        public ImageRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<Image> AllImage => applicationDb.Image;

        public IEnumerable<Image> HomeGalleryImage => applicationDb.Image.Where(i => i.ImageTypeId == 1);

        public IEnumerable<Image> HomeNewImage => applicationDb.Image.Where(i => i.ImageTypeId == 2);

    }
}