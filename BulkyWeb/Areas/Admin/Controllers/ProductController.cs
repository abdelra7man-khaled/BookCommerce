using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IProductRepository _productRepository, ICategoryRepository _categoryRepository, IWebHostEnvironment _webHostEnviroment) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll().ToList();

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

                if (productVM.Product.Id == 0)
                {
                    _productRepository.Add(productVM.Product);
                }
                else
                {
                    _productRepository.Update(productVM.Product);
                }
                _productRepository.Save();
                TempData["success"] = $"Product created successfully";
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
                return View("Create");
            }
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id is null || id == 0)
        //        return NotFound();

        //    Product? productToEdit = _productRepository.Get(c => c.Id == id);

        //    if (productToEdit is null)
        //        return NotFound();

        //    return View(productToEdit);
        //}

        //[HttpPost]
        //public IActionResult SaveEdit(Product productToEdit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _productRepository.Update(productToEdit);
        //        _productRepository.Save();
        //        TempData["success"] = "Product updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View("Edit");
        //}

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
