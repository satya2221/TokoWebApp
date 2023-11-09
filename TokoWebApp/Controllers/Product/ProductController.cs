using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoWebApp.Models;
using TokoWebApp.Services.Interfaces;

namespace TokoWebApp.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<ProductModel> products = await _productService.GetProductsAsync();
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            var result = (from prod in products
                          join cat in categories
                          on prod.CatId equals cat.CategoryId
                          select new ProductModel
                          {
                             ProductId = prod.ProductId,
                             Cat = cat,
                             ProductName = prod.ProductName,
                             Stock = prod.Stock,
                             Price = prod.Price,
                             CatId = prod.CatId,
                          }).ToList();
            return View(result);
        }
        public async Task<ActionResult> IndexAdmin()
        {
            List<ProductModel> products = await _productService.GetProductsAsync();
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            var result = (from prod in products
                          join cat in categories
                          on prod.CatId equals cat.CategoryId
                          select new ProductModel
                          {
                             ProductId = prod.ProductId,
                             Cat = cat,
                             ProductName = prod.ProductName,
                             Stock = prod.Stock,
                             Price = prod.Price,
                             CatId = prod.CatId,
                          }).ToList();
            return View(result);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ProductModel product = await _productService.GetProductById(id);
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            ViewBag.product = product;
            ViewBag.Categories = categories;
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel product)
        {
            try
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Transaction(Guid id)
        {
            ProductModel product = await _productService.GetProductById(id);
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            ViewBag.product = product;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Transaction(ProductModel product, int qty)
        {
            product.Stock = product.Stock - qty;
            try
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: ProductController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
