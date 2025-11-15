using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController(AppDbContext _context) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> cotegories = _context.Categories.ToList();
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
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToEdit = _context.Categories.Find(id);

            if (categoryToEdit is null)
                return NotFound();

            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult SaveEdit(Category categoryToEdit)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(categoryToEdit);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToDelete = _context.Categories.Find(id);

            if (categoryToDelete is null)
                return NotFound();

            return View(categoryToDelete);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Category? categoryToDelete = _context.Categories.Find(id);
            if (categoryToDelete is null)
                return NotFound();

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
