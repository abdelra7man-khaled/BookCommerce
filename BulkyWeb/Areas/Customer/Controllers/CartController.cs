using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController(IUnitOfWork _unitOfWork) : Controller
    {
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    c => c.ApplicationUserId == userId,
                    includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }

        [HttpGet]
        public IActionResult Summary()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId,
                    includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId)!;

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber!;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City!;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State!;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress!;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode!;

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }


            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId,
                    includeProperties: "Product");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId!;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId)!;


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0) // if is a regluar customer
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusPending;
            }
            else // if is a company user
            {
                ShoppingCartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusApproved;
            }
            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }


            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {

            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
        }


        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
        public IActionResult IncreaseQuantity(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            if (cart is null)
            {
                return NotFound("Cart not found");
            }
            cart.Count += 1;
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecreaseQuantity(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            if (cart is null)
            {
                return NotFound("Cart not found");
            }

            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                cart.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cart);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteFromCart(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId, includeProperties: "Product");
            if (cart is null)
            {
                return NotFound("Cart not found");
            }

            string productName = cart.Product?.Title ?? string.Empty;

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            TempData["success"] = $" {productName} product deleted successfully from your cart";
            return RedirectToAction(nameof(Index));
        }

        private decimal GetPriceBasedOnQuantity(ShoppingCart cart)
        {
            if (cart.Count <= 50)
            {
                return cart.Product.Price;
            }
            else if (cart.Count <= 100)
            {
                return cart.Product.Price50;
            }
            else
            {
                return cart.Product.Price100;
            }
        }
    }
}
