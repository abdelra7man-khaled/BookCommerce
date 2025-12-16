using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
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

        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(o => o.Id == orderId, includeProperties: "ApplicationUser")!,
                OrderDetails = _unitOfWork.OrderDetail.GetAll(o => o.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(orderVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll([FromQuery] string status)
        {
            IEnumerable<OrderHeader> orderHeaders = _unitOfWork.OrderHeader
                        .GetAll(includeProperties: "ApplicationUser")
                        .ToList();

            switch (status)
            {
                case "inprocess":
                    orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusInProcess);
                    break;
                case "pending":
                    orderHeaders = orderHeaders.Where(o => o.PaymentStatus == StaticDetails.PaymentStatusDelayedPayment);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(o => o.OrderStatus == StaticDetails.StatusApproved);
                    break;
                default:
                    break;
            }

            return Json(new { data = orderHeaders });
        }


        #endregion
    }
}
