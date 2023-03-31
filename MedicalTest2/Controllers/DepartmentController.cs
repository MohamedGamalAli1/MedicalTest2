using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IStoreRepo<Department> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public DepartmentController(IStoreRepo<Department> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: DepartmentController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department result)
        {
            try
            {
                repo.Add(result);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department result)
        {
            try
            {
                repo.Update(id, result);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department result)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.DepartmentId == result.Id);
                if (!isExists)
                {
                    repo.Delete(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var department = repo.GetById(id);
                    return View(department);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
