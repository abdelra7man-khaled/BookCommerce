using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.ViewComponents
{
    public class ShoppingCartViewComponent(IUnitOfWork _unitOfWork) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is not null)
            {
                if (HttpContext.Session.GetInt32(StaticDetails.SessionCart) is null)
                {
                    HttpContext.Session.SetInt32("SessionCart",
                    _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).Count());
                }
                return View(HttpContext.Session.GetInt32(StaticDetails.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }


        }
    }
}
