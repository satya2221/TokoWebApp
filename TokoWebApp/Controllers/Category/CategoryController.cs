using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokoWebApp.Models;
using TokoWebApp.Services.Interfaces;

namespace TokoWebApp.Controllers.Category
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            List<CategoryModel> categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                _categoryService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            CategoryModel category = await _categoryService.GetCategoryById(id);
            ViewBag.categories = category;
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel category)
        {
            try
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CategoryController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
