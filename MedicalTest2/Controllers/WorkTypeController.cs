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
    public class WorkTypeController : Controller
    {
        private readonly IStoreRepo<WorkType> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public WorkTypeController(IStoreRepo<WorkType> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: WorkTypeController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: WorkTypeController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: WorkTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkType result)
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

        // GET: WorkTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: WorkTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkType result)
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

        // GET: WorkTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: WorkTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, WorkType result)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.WorkTypeId == result.Id);
                if (!isExists)
                {
                    repo.Delete(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var workType = repo.GetById(id);
                    return View(workType);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
