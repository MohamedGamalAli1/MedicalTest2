using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.Services;
using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    public class EmployeeAttachmentController : Controller
    {
        private readonly IStoreRepo<EmployeeAttachment> repo;
        private readonly IStoreRepo<Employee> repoEmployee;
        private readonly ISMSService smsService;
        private readonly IStoreRepo<Attachment> attachmentRepo;
        public EmployeeAttachmentController(IStoreRepo<EmployeeAttachment> repo, IStoreRepo<Employee> repoEmployee, ISMSService smsService, IStoreRepo<Attachment> attachmentRepo)
        {
            this.repo = repo;
            this.repoEmployee = repoEmployee;
            this.smsService = smsService;
            this.attachmentRepo = attachmentRepo;
        }
        // GET: EmployeeAttachmentController
        public ActionResult Index()
        {
            var result = repo.Get();
            return View(result);
        }

        // GET: EmployeeAttachmentController/Details/5
        public ActionResult Details(int employeeId)
        {
            var result = repo.Get().Where(r => r.EmployeeId == employeeId);
            var employee = repoEmployee.GetById(employeeId);
            EmployeeAttachmentViewModel employeeAttachmentViewModel = new EmployeeAttachmentViewModel()
            {
                EmployeeAttachment = result.ToList(),
                EmployeeId = employeeId,
                Employee = employee
            };
            return View(employeeAttachmentViewModel);
        }
        public ActionResult Send(int id)
        {
            var result = repo.GetById(id);


            //var result = repo.Get().Where(r => r.EmployeeId == employeeAttachment.EmployeeId);
            var employee = repoEmployee.GetById(result.EmployeeId);


            result.Employee = employee;

            SendSMSDto sendSMSDto = new SendSMSDto() { EmployeeAttachment = result };
            return View(sendSMSDto);
        }
        [HttpPost]
        public IActionResult Send(SendSMSDto dto)
        {
            var employeeAttachment = repo.GetById(dto.EmployeeAttachment.Id);
            var employee = repoEmployee.GetById(employeeAttachment.EmployeeId);


            employeeAttachment.Employee = employee;
            var number = "+2" + employeeAttachment.Employee.PhoneNumber;
            // var number = "+201557808410";
            //TODO change country code +2

            var result = smsService.Send(number, dto.Body);

            if (!string.IsNullOrEmpty(result.Result.ErrorMessage))
                return BadRequest(result.Result.ErrorMessage);
            else
            {
                employeeAttachment.IsSent = true;
                employeeAttachment.SentDate = DateTime.UtcNow;
                repo.Update(employeeAttachment.Id, employeeAttachment);
                EmployeeAttachmentViewModel employeeAttachmentViewModel = new EmployeeAttachmentViewModel()
                {
                    EmployeeAttachment = new List<EmployeeAttachment>()
                {
                    employeeAttachment,
                },
                    EmployeeId = employeeAttachment.EmployeeId,
                    Employee = employee
                };
                return View(nameof(Details), employeeAttachmentViewModel);
            }

        }
        // GET: EmployeeAttachmentController/Create
        public ActionResult Create(int employeeId)
        {
            AddEmployeeAttachmentViewModel result = new AddEmployeeAttachmentViewModel()
            {
                //EmployeeAttachment = new EmployeeAttachment(),
                Attachments = SelectDdlAttachment(),
                EmployeeId = employeeId,
            };
            return View(result);
        }
        public List<Attachment> SelectDdlAttachment()
        {
            var result = attachmentRepo.Get().ToList();
            result.Insert(0, new Attachment() { Id = -1, Name = "---- اختر مرفق -------" });
            return result;
        }
        // POST: EmployeeAttachmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEmployeeAttachmentViewModel result)
        {
            try
            {
                var employeeAttachement = new EmployeeAttachment()
                {
                    From = result.From,
                    To = result.To,
                    EmployeeId = result.EmployeeId,
                    IsSent = false,
                    AttachmentId = result.AttachmentId,
                };
                var isExists = repo.Get().Any(r => r.AttachmentId == result.AttachmentId && r.EmployeeId == result.EmployeeId);
                if (!isExists)
                {
                    repo.Add(employeeAttachement);
                    var employee = repoEmployee.GetById(employeeAttachement.EmployeeId);
                    EmployeeAttachmentViewModel employeeAttachmentViewModel = new EmployeeAttachmentViewModel()
                    {
                        EmployeeAttachment = new List<EmployeeAttachment>()
                {
                    employeeAttachement,
                },
                        EmployeeId = employeeAttachement.EmployeeId,
                        Employee = employee
                    };
                    return View(nameof(Details), employeeAttachmentViewModel);
                }
                else
                {
                    ModelState.AddModelError("", "مضاف من قبل");
                    AddEmployeeAttachmentViewModel employeeAttachmentViewModel = new AddEmployeeAttachmentViewModel()
                    {
                        Attachments = SelectDdlAttachment(),
                        EmployeeId = result.EmployeeId,
                    };
                    return View(employeeAttachmentViewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeAttachmentController/Edit/5
        public ActionResult Edit(int id)
        {
            var employeeAttachment = repo.GetById(id);
            var attachment = attachmentRepo.GetById(employeeAttachment.AttachmentId);
            var attachments = attachmentRepo.Get().Where(r => r.Id == employeeAttachment.AttachmentId).ToList();

            AddEmployeeAttachmentViewModel result = new AddEmployeeAttachmentViewModel()
            {
                //EmployeeAttachment = new EmployeeAttachment(),
                Attachments = attachments,
                AttachmentId = attachment.Id,
                EmployeeId = employeeAttachment.EmployeeId,
                To = employeeAttachment.To,
                From = employeeAttachment.From,
            };
            return View(result);
        }

        // POST: EmployeeAttachmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AddEmployeeAttachmentViewModel result)
        {
            try
            {
                var employeeAttachments = repo.Get();
                var employeeAttachment = repo.GetById(id);
                employeeAttachment.From = result.From;
                employeeAttachment.To = result.To;
                repo.Update(id, employeeAttachment);
                var employee = repoEmployee.GetById(result.EmployeeId);
                EmployeeAttachmentViewModel employeeAttachmentViewModel = new EmployeeAttachmentViewModel()
                {
                    Employee=employee,
                    EmployeeAttachment= employeeAttachments,
                    EmployeeId=result.EmployeeId,
                };
                return View(nameof(Details), employeeAttachmentViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeAttachmentController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = repo.GetById(id);
            return View(result);
        }

        // POST: EmployeeAttachmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeAttachment result)
        {
            try
            {
                repo.Delete(result);
                EmployeeAttachmentViewModel employeeAttachmentViewModel = new EmployeeAttachmentViewModel()
                {
                    EmployeeAttachment = new List<EmployeeAttachment>()
                {
                    result,
                },
                    EmployeeId = result.EmployeeId,
                    Employee = result.Employee
                };
                return View(nameof(Details), employeeAttachmentViewModel);


                //var isExists = repoEmployee.Get().Any(r => r.EmployeeAttachmentId == result.Id);
                //if (!isExists)
                //{
                //    repo.Delete(result);
                //    return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    ModelState.AddModelError("", "لايمكن الحذف يوجد موظفين على هذا التصنيف");
                //    var EmployeeAttachment = repo.GetById(id);
                //    return View(EmployeeAttachment);
                //}

            }
            catch
            {
                return View();
            }
        }
    }
}
