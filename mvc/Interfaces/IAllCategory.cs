using mvc.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvc.Interfaces {
    public interface IAllCategory
    {
        IEnumerable<Category> AllCategory { get; }

        IEnumerable<Category> IsPublishedCategory { get; }
    }
}
