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
    public class NationalityController : Controller
    {
        private readonly IStoreRepo<Nationality> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public NationalityController(IStoreRepo<Nationality> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: NationalityController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: NationalityController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: NationalityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NationalityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nationality nationality)
        {
            try
            {
                repo.Add(nationality);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NationalityController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: NationalityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Nationality nationality)
        {
            try
            {
                repo.Update(id, nationality);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NationalityController/Delete/5
        public ActionResult Delete(int id)
        {
            var nationality = repo.GetById(id);
            return View(nationality);
        }

        // POST: NationalityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Nationality nationality)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.NationalityId == nationality.Id);
                if (!isExists)
                {
                    repo.Delete(nationality);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var result = repo.GetById(id);
                    return View(result);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
