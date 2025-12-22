using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class UserController(IUnitOfWork _unitOfWork, RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            RoleManagementVM roleManagementVM = new()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company")!,
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
                Companies = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            roleManagementVM.ApplicationUser.Role = _userManager
                .GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId))
                .GetAwaiter().GetResult().FirstOrDefault()!;

            return View(roleManagementVM);
        }

        [HttpPost]
        public IActionResult SaveUpdate(RoleManagementVM roleManagementVM)
        {
            string previosRole = _userManager
                .GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id))
                .GetAwaiter().GetResult().FirstOrDefault()!;

            ApplicationUser appUser = _unitOfWork.ApplicationUser
                    .Get(u => u.Id == roleManagementVM.ApplicationUser.Id)!;

            // role is updated by admin
            if (previosRole != roleManagementVM.ApplicationUser.Role)
            {
                if (roleManagementVM.ApplicationUser.Role == StaticDetails.Role_Company)
                {
                    appUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                if (previosRole == StaticDetails.Role_Company)
                {
                    appUser.CompanyId = null;
                }
                _unitOfWork.ApplicationUser.Update(appUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(appUser, previosRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(appUser,
                                roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();

            }
            else
            {
                if (previosRole == StaticDetails.Role_Company &&
                    appUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
                {
                    appUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(appUser);
                    _unitOfWork.Save();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            foreach (var user in users)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault()!;

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
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (user is null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (user.LockoutEnd is not null && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                // user is currently locked, we will unlock them
                user.LockoutEnd = DateTimeOffset.UtcNow;
            }
            else
            {
                // user is currently unlocked, we will lock them
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            }
            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
