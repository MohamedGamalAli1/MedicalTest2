using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = GetRoles();
            return View(roles);
        }

        private List<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", GetRoles());
            var isExists = await roleManager.RoleExistsAsync(model.Name);
            if (isExists)
            {
                ModelState.AddModelError("Name", "Role is already exists !");
                return View("Index", GetRoles());
            }
            await roleManager.CreateAsync(new IdentityRole() { Name = model.Name.Trim() });
            return RedirectToAction(nameof(Index));
        }
    }
}
