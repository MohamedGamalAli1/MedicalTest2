using MedicalTest2.Models;
using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<MyUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UsersController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.Select(r => new UserViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                //Roles = roleManager.Roles.Select(r => r.Name).ToList()
                Roles = userManager.GetRolesAsync(r).Result
            }).ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.Select(r => new RoleViewModelCmBox() { RoleId = r.Id, RoleName = r.Name }).ToListAsync();
            var viewmModel = new AddUserViewModel()
            {
                RolesCmBoxes = roles
            };
            return View(viewmModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.RolesCmBoxes.Any(r => r.IsChecked))
            {
                ModelState.AddModelError("RolesCmBoxes", "You Have to check one role at least");
                return View(model);
            }
            if (userManager.Users.Any(r => r.Email == model.Email))
            {
                ModelState.AddModelError("Users", "Email is already exists");
                return View(model);
            }
            if (await userManager.FindByNameAsync(model.Name) != null)
            {
                ModelState.AddModelError("Users", "Name is already exists");
                return View(model);
            }
            var user = new MyUser { Name = model.Name, UserName = model.Name, Email = model.Email, Password = model.Password };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await userManager.AddToRolesAsync(user, model.RolesCmBoxes.Where(r => r.IsChecked).Select(r => r.RoleName));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("RolesCmBoxes", error.Description);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await roleManager.Roles.ToListAsync();
            //roleManager.Roles.Where(r=>r.Id==)
            var viewmModel = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.Name,
                RolesCmBoxes = roles.Select(r => new RoleViewModelCmBox()
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsChecked = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View(viewmModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var model = new EditViewModel()
            {
                Id = userId,
                Email = user.Email,
                Name = user.Name,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user =await userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();
            var isEmailExits = await userManager.FindByEmailAsync(model.Email);
            if(isEmailExits!=null && isEmailExits.Id != model.Id)
            {
                ModelState.AddModelError("Email", "Email is already assigned to another user");
                return View(model);
            }
            var isNameExits = await userManager.FindByNameAsync(model.Name);
            if(isNameExits != null && isNameExits.Id != model.Id)
            {
                ModelState.AddModelError("Name", "Name is already assigned to another user");
                return View(model);
            }
            user.Name = model.Name;
            user.Email = model.Email;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel userRolesViewModel)
        {
            var user = await userManager.FindByIdAsync(userRolesViewModel.UserId);
            if (user == null)
                return NotFound();
            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in userRolesViewModel.RolesCmBoxes)
            {
                if (!item.IsChecked && roles.Any(r => r == item.RoleName))
                {
                    await userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
                if (item.IsChecked && !roles.Any(r => r == item.RoleName))
                {
                    await userManager.AddToRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
