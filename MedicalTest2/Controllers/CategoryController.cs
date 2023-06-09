﻿using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IStoreRepo<Category> repo;
        private readonly IStoreRepo<Employee> repoPatient;
        public CategoryController(IStoreRepo<Category> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoPatient = repoPatient;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var categories = repo.Get();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = repo.GetById(id);
            return View(category);
        }

        // GET: categoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                repo.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = repo.GetById(id);
            return View(category);
        }

        // POST: categoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                repo.Update(id, category);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = repo.GetById(id);
            return View(category);
        }

        // POST: CategoryController/Delete/5
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                var catDeletedId=repo.GetById(id).Id;
               var isHavePatients= repoPatient.Get().Any(r => r.CategoryId == catDeletedId);
                if(isHavePatients)
                {
                    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                    return View(category);
                }
                else
                {
                    repo.Delete(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
