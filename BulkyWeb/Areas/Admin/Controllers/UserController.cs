using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class UserController(AppDbContext _context) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _context.ApplicationUsers.Include(u => u.Company).ToList();

            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (var user in users)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId)?.Name!;

                if (user.Company is null)
                {
                    user.Company = new() { Name = string.Empty };
                }
            }
            return Json(new { data = users });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            return Json(new { success = true, message = "the company deleted successfully" });
        }

        #endregion
    }
}
