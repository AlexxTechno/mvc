using mvc.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Interfaces
{
    public interface IAllImage
    {
        IEnumerable<Image> AllImage { get; }

        IEnumerable<Image> HomeGalleryImage { get; }

        IEnumerable<Image> HomeNewImage { get; }

    }
}