using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IProductRepository _productRepository, ICategoryRepository _categoryRepository) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll().ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _categoryRepository.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult SaveCreate(ProductVM newProduct)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(newProduct.Product);
                _productRepository.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                newProduct.CategoryList = _categoryRepository.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View("Create");
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Product? productToEdit = _productRepository.Get(c => c.Id == id);

            if (productToEdit is null)
                return NotFound();

            return View(productToEdit);
        }

        [HttpPost]
        public IActionResult SaveEdit(Product productToEdit)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(productToEdit);
                _productRepository.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Product? productToDelete = _productRepository.Get(c => c.Id == id);

            if (productToDelete is null)
                return NotFound();

            return View(productToDelete);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Product? productToDelete = _productRepository.Get(c => c.Id == id);
            if (productToDelete is null)
                return NotFound();

            _productRepository.Remove(productToDelete);
            _productRepository.Save();
            TempData["success"] = "Product Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
