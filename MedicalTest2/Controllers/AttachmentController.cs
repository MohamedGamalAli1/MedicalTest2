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
    public class AttachmentController : Controller
    {
        private readonly IStoreRepo<Attachment> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        public AttachmentController(IStoreRepo<Attachment> repo, IStoreRepo<Employee> repoPatient)
        {
            this.repo = repo;
            this.repoEmployee = repoPatient;
        }
        // GET: AttachmentController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: AttachmentController/Details/5
        public ActionResult Details(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // GET: AttachmentController/Create
        public ActionResult Create()
        {
            Attachment result = new Attachment();
            return View(result);
        }

        // POST: AttachmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attachment result)
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

        // GET: AttachmentController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: AttachmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Attachment result)
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

        // GET: AttachmentController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: AttachmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Attachment result)
        {
            try
            {
                repo.Delete(result);
                return RedirectToAction(nameof(Index));


                //var isExists = repoEmployee.Get().Any(r => r.AttachmentId == result.Id);
                //if (!isExists)
                //{
                //    repo.Delete(result);
                //    return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                //    var Attachment = repo.GetById(id);
                //    return View(Attachment);
                //}

            }
            catch
            {
                return View();
            }
        }
    }
}
