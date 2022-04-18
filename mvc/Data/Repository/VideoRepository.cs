using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using mvc.Interfaces;
using mvc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Data.Repository
{
    public class VideoRepository : IAllVideo
    {
        private readonly ApplicationDbContext applicationDb;

        public VideoRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<Video> AllVideo => applicationDb.Video;
    }
}
