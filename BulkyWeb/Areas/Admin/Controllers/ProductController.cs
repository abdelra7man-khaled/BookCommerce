using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IWebHostEnvironment _webHostEnviroment) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll(includeProperties: "Category").ToList();

            return View(products);
        }

        public IActionResult Upsert(int? id) // Update || Create
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

            if (id == null || id == 0)
            {
                // Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _productRepository.Get(p => p.Id == id)!;
                if (productVM.Product == null)
                    return NotFound();

                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult SaveChanges(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\products\" + fileName;
                }

                bool isNewProduct = false;
                if (productVM.Product.Id == 0)
                {
                    _productRepository.Add(productVM.Product);
                    isNewProduct = true;
                }
                else
                {
                    _productRepository.Update(productVM.Product);
                }
                _productRepository.Save();
                TempData["success"] = $"Product {(isNewProduct ? "created" : "updated")} successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _categoryRepository.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View("Upsert");
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _productRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToDelete = _productRepository.Get(u => u.Id == id);
            if (productToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string oldImagePath = Path.Combine(_webHostEnviroment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _productRepository.Remove(productToDelete);
            _productRepository.Save();

            return Json(new { success = true, message = "the product deleted successfully" });
        }

        #endregion
    }
}
