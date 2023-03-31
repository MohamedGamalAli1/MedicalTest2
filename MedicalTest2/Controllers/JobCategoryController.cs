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
    public class JobCategoryController : Controller
    {
        private readonly IStoreRepo<JobCategory> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public JobCategoryController(IStoreRepo<JobCategory> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: JobCategoryController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: JobCategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: JobCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCategory result)
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

        // GET: JobCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: JobCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobCategory result)
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

        // GET: JobCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: JobCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, JobCategory result)
        {
            try
            {
                var isExists = repoEmployee.Get().Any(r => r.JobCategoryId == result.Id);
                if (!isExists)
                {
                    repo.Delete(result);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    var jobCategory = repo.GetById(id);
                    return View(jobCategory);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
