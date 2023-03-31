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
    public class DestinationController : Controller
    {
        private readonly IStoreRepo<Destination> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public DestinationController(IStoreRepo<Destination> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: DestinationController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: DestinationController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: DestinationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DestinationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Destination result)
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

        // GET: DestinationController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: DestinationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Destination result)
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

        // GET: DestinationController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: DestinationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Destination result)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.DestinationId == result.Id);
                if (!isExists)
                {
                    repo.Delete(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var destination = repo.GetById(id);
                    return View(destination);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
