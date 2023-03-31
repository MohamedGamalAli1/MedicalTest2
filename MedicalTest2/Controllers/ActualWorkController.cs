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
    public class ActualWorkController : Controller
    {
        private readonly IStoreRepo<ActualWork> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public ActualWorkController(IStoreRepo<ActualWork> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: ActualWorkController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: ActualWorkController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: ActualWorkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActualWorkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActualWork result)
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

        // GET: ActualWorkController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: ActualWorkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ActualWork result)
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

        // GET: ActualWorkController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: ActualWorkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ActualWork result)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.ActualWorkId == result.Id);
                if (!isExists)
                {
                    repo.Delete(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var actualWork = repo.GetById(id);
                    return View(actualWork);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
