using Entities.Concrete;
using Entities.Concrete.Dto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_Store.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            List<IdentityRole> roles = new List<IdentityRole>();
            roles.AddRange(_roleManager.Roles);
            return View(roles);
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
            if (result.Succeeded)
            {
                TempData["Ok"] = "Role Eklendi..";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AssignedRole(string RoleName)
        {
            IdentityRole role = await _roleManager.FindByNameAsync(RoleName);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (var user in _userManager.Users.ToList())
            {
                var list = await _userManager.IsInRoleAsync(user, RoleName) ? hasRole : hasNotRole;

                list.Add(user);
            }
            
            AssignedRoleDTO assignedRole = new AssignedRoleDTO()
            {
                Role = role,
                RoleName = RoleName,
                HasNotRoles = hasNotRole,
                HasRoles = hasRole,
                
            };
            

            return View(assignedRole);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedRole(AssignedRoleDTO mel)
        {
            foreach (var item in mel.AddIds ?? new string[] {})
            {
                AppUser user = await _userManager.FindByIdAsync(item);
                await _userManager.AddToRoleAsync(user,mel.RoleName);
            }

            foreach (var item in mel.DeletedIds ?? new string[] {}) 
            {
                AppUser user = await _userManager.FindByIdAsync(item);
                await _userManager.RemoveFromRoleAsync(user,mel.RoleName);
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> deleteRole(string roleName)
        {
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("index");
        }
    }
}
