using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Interfaces;
using mvc.Models;

namespace mvc.Data.Repository
{
    public class ImageTypeRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public ImageTypeRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<ImageType> AllImageType => applicationDb.ImageType;
    }
}
