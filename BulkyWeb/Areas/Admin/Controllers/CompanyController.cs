using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController(IUnitOfWork _unitOfWork) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();

            return View(companies);
        }

        public IActionResult Upsert(int? id) // Update || Create
        {

            if (id == null || id == 0)
            {
                // Create
                return View(new Company());
            }
            else
            {
                // Update
                Company company = _unitOfWork.Company.Get(p => p.Id == id)!;
                if (company == null)
                    return NotFound();

                return View(company);
            }
        }

        [HttpPost]
        public IActionResult SaveChanges(Company company)
        {
            if (ModelState.IsValid)
            {

                bool isNewCompany = false;
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    isNewCompany = true;
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = $"Company {(isNewCompany ? "created" : "updated")} successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Upsert");
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToDelete = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "the company deleted successfully" });
        }

        #endregion
    }
}
