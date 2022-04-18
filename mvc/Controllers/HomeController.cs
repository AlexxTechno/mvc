using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllProduct _allProduct;
        private readonly IAllCategory _allCategory;

        public HomeController(IAllProduct iAllProduct, IAllCategory iAllCategory)
        {
            _allProduct = iAllProduct;
            _allCategory = iAllCategory;
        }

        [Route("")]
        public IActionResult Index()
        {
            ProductCategory prodcat = new ProductCategory
            {
                Cat = _allCategory.IsPublishedCategory,
                Prod = _allProduct.IsPublishedProduct
            };
            return View(prodcat);     // было return View(); 
        }
    }
}
