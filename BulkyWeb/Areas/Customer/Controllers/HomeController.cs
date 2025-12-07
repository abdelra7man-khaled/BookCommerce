using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(p => p.Id == id, includeProperties: "Category")!,
                ProductId = id,
                Count = 1
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddProductToCart(ShoppingCart cart)
        {
            var claimsIdentity = User.Identity! as ClaimsIdentity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            cart.ApplicationUserId = userId!;

            ShoppingCart? userCart = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == userId &&
            c.ProductId == cart.ProductId);

            if (userCart is null)
            {
                _unitOfWork.ShoppingCart.Add(cart);
            }
            else
            {
                userCart.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(userCart);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
