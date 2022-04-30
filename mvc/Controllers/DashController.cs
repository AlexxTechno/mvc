using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Interfaces;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Controllers
{
    [Authorize]
    public class DashController : Controller
    {
        private readonly IAllProduct _allProduct;
        private readonly IAllCategory _allCategory;
        //private readonly IAllCategoryCatalog _allCategoryCatalog;
        private readonly ApplicationDbContext _applicationDb;

        public DashController(ApplicationDbContext applicationDb, IAllProduct iAllProduct, IAllCategory iAllCategory)       //, IAllCategoryCatalog iAllCategoryCatalog
        {
            _allProduct = iAllProduct;
            _allCategory = iAllCategory;
            //_allCategoryCatalog = iAllCategoryCatalog;
            _applicationDb = applicationDb;
        }

        ///*******************************************************************
        ///---------- Домашняя страница админки -----------
        /// <summary>
        /// Home Page Dashboard
        /// </summary>
        /// <returns></returns>
        [Route("/dash")]
        public IActionResult Index()
        {
            return View();      // было return View(); 
        }

        ///*******************************************************************
        ///---------- CRUD for Products -----------
        /// <summary>
        /// Show All Products
        /// </summary>
        /// <returns></returns>
        [Route("/dash/products")]
        public IActionResult Products()
        {
            ProductCategory prodcat = new ProductCategory
            {
                Cat = _allCategory.AllCategory,
                Prod = _allProduct.AllProduct
            };
            return View(prodcat);
        }

        /// <summary>
        /// Create New Product and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/prodnew")]
        public async Task<IActionResult> ProdNew()
        {
            if (Request.Form["title"] != "")
            {
                //-- создаем новое изделие --
                Product prod = new Product();
                prod.Title = Request.Form["title"];
                prod.Number = Convert.ToInt32(Request.Form["number"]);
                prod.Sku = Request.Form["sku"];
                prod.Dimension = Request.Form["dimension"];
                prod.Price = Convert.ToInt32(Request.Form["price"]);
                prod.PriceBig = Convert.ToInt32(Request.Form["pricebig"]);
                prod.Description = Request.Form["description"];
                prod.Summary = Request.Form["summary"];
                prod.Materials = Request.Form["materials"];
                if (Request.Form["isnew"] == "on") { prod.IsNew = true; }
                else { prod.IsNew = false; }
                if (Request.Form["ispublished"] == "on") { prod.IsPublished = true; }
                else { prod.IsPublished = false; }
                prod.CategoryId = Convert.ToInt32(Request.Form["category"]);

                try
                {
                    await _applicationDb.AddAsync(prod);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось внести изменения в модель Product.");
                }

                int id = _applicationDb.Product.Max(prod => prod.Id);

                //-- вносим изменения в картинки --
                //- основная на главной                             
                Image imghome = new Image();
                imghome.ProductId = id;
                imghome.ImageTypeId = 1;
                imghome.Guide = Request.Form["imghome"];
                try
                {
                    await _applicationDb.AddAsync(imghome);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинка на домашней.");
                }

                //- картинка на странице модели - под видео
                Image imgone = new Image();
                imgone.ProductId = id;
                imgone.ImageTypeId = 3;
                imgone.Guide = Request.Form["imgone"];

                try
                {
                    await _applicationDb.AddAsync(imgone);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинка на странице товара под видео.");
                }

                //- новые картинки на странице одной модели - галерея nullable
                if (Request.Form["imggal"] != "" && Request.Form["quansel"] != "0")
                {
                    Image imggal = new Image();
                    imggal.ProductId = id;
                    imggal.ImageTypeId = 4; ;
                    imggal.Guide = Request.Form["imggal"];
                    imggal.Quan = Convert.ToInt32(Request.Form["quansel"]);

                    try
                    {
                        await _applicationDb.AddAsync(imggal);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинки на странице товара галерея.");
                    }
                }

                //- новая запись в видео - nullable                 
                if (Request.Form["video"] != "")
                {
                    Video video = new Video();
                    video.ProductId = id;
                    video.Guide = Request.Form["video"];

                    try
                    {
                        await _applicationDb.AddAsync(video);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Video.");
                    }
                }
            }
            return Redirect("~/Dash/Products");
        }

        /// <summary>
        /// Edit Product and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/prodedit")]
        public async Task<IActionResult> ProdEdit()
        {
            if (Request.Form["id"] != "")
            {
                //-- вносим изменения в изделие по id --
                int id = Convert.ToInt32(Request.Form["id"]);

                //- в это изделие
                Product prod = new Product { Id = id };
                // Product prod = _applicationDb.Product.Find(id);
                // var prod = await _applicationDb.Product.FirstOrDefaultAsync(p => p.Id == id);

                //- вносим изменения
                prod.Number = Convert.ToInt32(Request.Form["number"]);
                prod.Title = Request.Form["title"];
                prod.Sku = Request.Form["sku"];
                prod.Dimension = Request.Form["dimension"];
                prod.Price = Convert.ToInt32(Request.Form["price"]);
                prod.PriceBig = Convert.ToInt32(Request.Form["pricebig"]);
                prod.Description = Request.Form["description"];
                prod.Summary = Request.Form["summary"];
                prod.Materials = Request.Form["materials"];
                if (Request.Form["isnew"] == "on") { prod.IsNew = true; }
                else { prod.IsNew = false; }
                if (Request.Form["ispublished"] == "on") { prod.IsPublished = true; }
                else { prod.IsPublished = false; }
                prod.CategoryId = Convert.ToInt32(Request.Form["category"]);

                try
                {
                    _applicationDb.Update(prod);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось внести изменения в модель Product.");
                }

                System.Diagnostics.Debug.WriteLine("Вывод в консоль imghomeid из формы");
                System.Diagnostics.Debug.WriteLine(Request.Form["imghomeid"]);

                //-- вносим изменения в картинки --

                //- основная на главной - одна - должна быть                
                if (Request.Form["imghome"] != "" && Request.Form["imghomeid"] != "0")
                {
                    int imghomeid = Convert.ToInt32(Request.Form["imghomeid"]);
                    //int imghomeid = _applicationDb.Image.Where(i => i.ImageTypeId == 1)
                    //                                .Where(i => i.ProductId == id)
                    //                                .Select(i => i.Id)
                    //                                .FirstOrDefault();

                    Image my_imghome = new Image { Id = imghomeid };
                    my_imghome.Guide = Request.Form["imghome"];
                    my_imghome.ProductId = id;
                    my_imghome.ImageTypeId = 1;
                    my_imghome.Quan = 0;
                    try
                    {
                        _applicationDb.Update(my_imghome);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинка на домашней.");
                    }
                }

                //-картинка на странице модели - под видео - одна - должна быть
                if (Request.Form["imgone"] != "" && Request.Form["imgoneid"] != "0")
                {
                    int imgoneid = Convert.ToInt32(Request.Form["imgoneid"]);
                    //int imghomeid = _applicationDb.Image.Where(i => i.ImageTypeId == 1)
                    //                                .Where(i => i.ProductId == id)
                    //                                .Select(i => i.Id)
                    //                                .FirstOrDefault();

                    Image my_imgone = new Image { Id = imgoneid };
                    my_imgone.Guide = Request.Form["imgone"];
                    my_imgone.ProductId = id;
                    my_imgone.ImageTypeId = 3;
                    my_imgone.Quan = 0;
                    try
                    {
                        _applicationDb.Update(my_imgone);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинка на странице товара под видео.");
                    }
                }

                //-картинка на странице модели - галерея - не одна - nullable
                if (Request.Form["imggal"] != "" && Request.Form["quansel"] != 0)
                {
                    int imggalid = Convert.ToInt32(Request.Form["imggalid"]);
                    Image my_imggal = new Image(); // создаем новое
                    if (imggalid != 0)
                    {
                        my_imggal.Id = imggalid; // изменяем существующее если есть
                    }

                    my_imggal.Guide = Request.Form["imggal"];
                    my_imggal.ProductId = id;
                    my_imggal.ImageTypeId = 4;
                    my_imggal.Quan = Convert.ToInt32(Request.Form["quansel"]);
                    try
                    {
                        _applicationDb.Update(my_imggal);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Image картинки на странице товара галерея.");
                    }
                }
                else
                {
                    // условия для удаления записи
                    if (Request.Form["imggal"] == "" || Request.Form["quansel"] == 0)
                    {
                        int imggalid = Convert.ToInt32(Request.Form["imggalid"]);
                        if (imggalid != 0)
                        {
                            Image my_imggalid = new Image(); // 
                            my_imggalid.Id = imggalid; // удаляем существующее если есть

                            try
                            {
                                _applicationDb.Image.Remove(my_imggalid);
                                await _applicationDb.SaveChangesAsync();
                            }
                            catch (DbUpdateException /* ex */)
                            {
                                //Log the error (uncomment ex variable name and write a log.)
                                ModelState.AddModelError("", "Не получилось удалить Image - картинки на странице товара галерея.");
                            }
                        }
                    }
                }

                //-- вносим изменения в видео - одна запись - nullable
                if (Request.Form["video"] != "" && Request.Form["videoid"] != "")
                {
                    int videoid = Convert.ToInt32(Request.Form["videoid"]);
                    Video my_video = new Video();
                    if (videoid != 0)
                    {
                        my_video.Id = videoid;
                    }

                    my_video.Guide = Request.Form["video"];
                    my_video.ProductId = id;

                    try
                    {
                        _applicationDb.Update(my_video);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Video.");
                    }
                }
                // удаление видео, если есть запись и в поле формы пусто
                if (Request.Form["video"] == "" && Request.Form["videoid"] != "" && Request.Form["videoid"] != "0")
                {
                    int videoid = Convert.ToInt32(Request.Form["videoid"]);
                    Video my_video = new Video();
                    my_video.Id = videoid;

                    try
                    {
                        _applicationDb.Video.Remove(my_video);
                        await _applicationDb.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Не получилось внести изменения в модель Video.");
                    }
                }
            }
            return Redirect("~/Dash/Products");
        }

        /// <summary>
        /// Delete Product and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/proddel")]
        public async Task<IActionResult> ProdDel()
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            if (id != 0)
            {
                //-удаляем товар и все связанные картинки и видео - каскадно
                //var product = await _applicationDb.Product.FindAsync(id); // работает но для каскада не годится
                var product = _applicationDb.Product.Include(p => p.Image).Include(p => p.Video).FirstOrDefault(p => p.Id == id);
                try
                {
                    _applicationDb.Remove(product);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException) // ex
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось каскадно удалить Product и связанные записи Image, Video.");
                }               
            }
            
            return Redirect("~/Dash/Products");
        }

        ///*******************************************************************
        ///---------- CRUD for Categories -----------
        /// <summary>
        /// Show All Categories
        /// </summary>
        /// <returns></returns>
        [Route("/dash/categories")]
        public IActionResult Categories()
        {
            var category = _allCategory.AllCategory;
            return View(category);
        }
        /// <summary>
        /// Create New Category and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/catnew")]
        public async Task<IActionResult> CatNew()
        {
            if (Request.Form["title"] != "")
            {
                Category cat = new Category();
                cat.Title = Request.Form["title"];
                cat.Description = Request.Form["description"];
                cat.Slug = Request.Form["slug"];
                cat.Number = Convert.ToInt32(Request.Form["number"]);
                if (Request.Form["ispublished"] == "on") { cat.IsPublished = true; }
                else { cat.IsPublished = false; }
                try 
                {
                    await _applicationDb.Category.AddAsync(cat);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось создать новую запись в модели Category.");
                }
            }
            return Redirect("~/Dash/Categories");
        }

        /// <summary>
        /// Edit Category and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/catedit")]
        public async Task<IActionResult> CatEdit()
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            if (id != 0)
            {
                var cat = await _applicationDb.Category.FindAsync(id);

                cat.Title = Request.Form["title"];
                cat.Slug = Request.Form["slug"];
                cat.Description = Request.Form["description"];
                cat.Number = Convert.ToInt32(Request.Form["number"]);
                if (Request.Form["ispublished"] == "on") { cat.IsPublished = true; }
                else { cat.IsPublished = false; }
                try
                {
                    _applicationDb.Update(cat);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось отредактировать категорию из модели Category.");
                }
            }
            return Redirect("~/Dash/Categories");
        }

        /// <summary>
        /// Delete Category and Redirect To All
        /// </summary>
        /// <returns></returns>
        [Route("/dash/catdel")]
        public async Task<IActionResult> CatDel()
        {            
            int id = Convert.ToInt32(Request.Form["id"]);
            if (id != 0)
            {
                // var cat = await _applicationDb.Category.FirstOrDefaultAsync(с => c.Id == id); // выдет ошибку
                var cat = await _applicationDb.Category.FindAsync(id); // работает

                try
                {
                    _applicationDb.Category.Remove(cat);
                    await _applicationDb.SaveChangesAsync();
                }
                catch (DbUpdateException)  //ex 
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Не получилось удалить категорию из модели Category.");
                }
            }                 
            return Redirect("~/Dash/Categories");
        }

        [Route("/dash/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        ///*******************************************************************
        ///---------- Catalog test controller -----------
        /// <summary>
        /// Show All Products
        /// </summary>
        /// <returns></returns>
        [Route("/dash/catalog")]
        public IActionResult Catalog()
        {
            ProductCategory prodcat = new ProductCategory
            {
                Cat = _allCategory.AllCategory,
                Prod = _allProduct.AllProduct
            };
            string img = prodcat.Prod.FirstOrDefault().Image.Where(img => img.ImageTypeId == 3).FirstOrDefault().Guide;
            int n = img.LastIndexOf("/")+1;
            ViewData["url"] = img.Substring(0, n);

            return View(prodcat);


            // IEnumerable<CategoryCatalog>   
            //var catalog = _allCategoryCatalog.IsPublishedCategoryCatalog;
            /*
            IEnumerable<CategoryCatalog> catalog = (IEnumerable<CategoryCatalog>)_applicationDb.Category.Where(cat => cat.IsPublished == true)
                                                                                       .Join(_applicationDb.Product.Where(prod => prod.IsPublished == true), // second set
                                                                                       cat => cat.Id,                       // first set selector
                                                                                       prod => prod.CategoryId,             // second set selector
                                                                                       (cat, prod) => new
                                                                                       {
                                                                                           Id = cat.Id,
                                                                                           Number = cat.Number,
                                                                                           Title = cat.Title,
                                                                                           Description = cat.Description,
                                                                                           ProductId = prod.Id,
                                                                                           IsPublished = prod.IsPublished,
                                                                                           ProductNumber = prod.Number,
                                                                                           ProductTitle = prod.Title,
                                                                                           Sku = prod.Sku,
                                                                                           IsNew = prod.IsNew,
                                                                                           Price = prod.Price,
                                                                                           PriceBig = prod.PriceBig,
                                                                                           ProductDescription = prod.Description,
                                                                                           Summary = prod.Summary,
                                                                                           Materials = prod.Materials,
                                                                                           ProductIsPublished = prod.IsPublished,
                                                                                       }).OrderBy(item => item.Number)
                                                                                       .ThenBy(item => item.ProductNumber)
                                                                                        .Join(_applicationDb.Image
                                                                                           .Where(img => img.ImageTypeId == 1),
                                                                                           product => product.ProductId,
                                                                                           img => img.ProductId,
                                                                                           (prod, image) => new
                                                                                           {
                                                                                               ImageId = image.Id,
                                                                                               ProductId = image.ProductId,
                                                                                               ImageGuide = image.Guide,
                                                                                               Quan = image.Quan
                                                                                           });
                                                                                        /*
                                                                                        .Join(_applicationDb.Video,
                                                                                           catalog => catalog.ProductId,
                                                                                           video => video.ProductId,
                                                                                           (prod, video) => new
                                                                                           {
                                                                                               VideoId = video.Id,
                                                                                               ProductId = video.ProductId,
                                                                                               VideoGuide = video.Guide
                                                                                           });                                                                            
            return View(catalog); */
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}