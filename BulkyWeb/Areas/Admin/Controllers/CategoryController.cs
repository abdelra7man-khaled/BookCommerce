using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController(IUnitOfWork _unitOfWork) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> cotegories = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(newCategory);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToEdit = _unitOfWork.Category.Get(c => c.Id == id);

            if (categoryToEdit is null)
                return NotFound();

            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult SaveEdit(Category categoryToEdit)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(categoryToEdit);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? categoryToDelete = _unitOfWork.Category.Get(c => c.Id == id);

            if (categoryToDelete is null)
                return NotFound();

            return View(categoryToDelete);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Category? categoryToDelete = _unitOfWork.Category.Get(c => c.Id == id);
            if (categoryToDelete is null)
                return NotFound();

            _unitOfWork.Category.Remove(categoryToDelete);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }


    }

}
