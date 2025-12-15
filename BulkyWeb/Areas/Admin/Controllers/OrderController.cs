using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = StaticDetails.Role_Admin)]
    public class OrderController(IUnitOfWork _unitOfWork) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = orderHeaders });
        }


        #endregion
    }
}
