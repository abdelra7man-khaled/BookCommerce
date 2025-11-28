using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController(ICategoryRepository _categoryRepository) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> cotegories = _categoryRepository.GetAll().ToList();
            return View(cotegories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCreate(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(newCategory);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToEdit = _categoryRepository.Get(c => c.Id == id);

            if (categoryToEdit is null)
                return NotFound();

            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult SaveEdit(Category categoryToEdit)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(categoryToEdit);
                _categoryRepository.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToDelete = _categoryRepository.Get(c => c.Id == id);

            if (categoryToDelete is null)
                return NotFound();

            return View(categoryToDelete);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Category? categoryToDelete = _categoryRepository.Get(c => c.Id == id);
            if (categoryToDelete is null)
                return NotFound();

            _categoryRepository.Remove(categoryToDelete);
            _categoryRepository.Save();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
