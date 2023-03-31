using MedicalTest2.Data;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IStoreRepo<Employee> repo;
        private readonly IStoreRepo<Category> CategoryRepo;
        private readonly IHostEnvironment env;
        private readonly List<string> permittedExcelExtensions;
        public EmployeeController(IStoreRepo<Employee> repo, IStoreRepo<Category> CategoryRepo, IHostEnvironment env)
        {
            this.repo = repo;
            this.CategoryRepo = CategoryRepo;
            this.env = env;
            permittedExcelExtensions = new List<string>() { ".xlsx", ".xls" };
        }
        public IActionResult Index()
        {
            return View(repo.Get());
        }

        public IActionResult UploadExcelData()
        {

            // var fi = new FileInfo(@"excel file.xlsx");

            return View();
        }
        [HttpPost]
        public IActionResult UploadExcelData(UploadExcelModel model)
        {
            try
            {
                ViewBag.isSaved = "";
                var isSaved = false;
                if (ModelState.IsValid)
                {
                    string fileName = string.Empty;
                    if (model.Url != null)
                    {
                        string url = Path.Combine(env.ContentRootPath, "wwwRoot/ExcelFiles");
                        fileName = model.Url.FileName;
                        var ext = Path.GetExtension(fileName).ToLowerInvariant();
                        if (!permittedExcelExtensions.Contains(ext) || string.IsNullOrEmpty(ext))
                        {
                            ViewBag.isSaved = "الامتداد غير مسموح";
                            return View(model);
                        }
                        string fi = Path.Combine(url, fileName);
                        model.Url.CopyToAsync(new FileStream(fi, FileMode.Create));
                        //   string fi = Path.Combine(env.ContentRootPath, "wwwRoot/excelFile.xlsx");
                        //var fi = new FileInfo(@"C:\Users\moham\source\repos\ReadExcel\ReadExcel\excelFile.xlsx");

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        List<string> headers = new List<string>() { "Id", "Name", "Description", "CategoryId", "ImageUrl" };
                        List<string> employees = new List<string>();
                        List<string> captionTitles = new List<string>();

                        using (var package = new ExcelPackage(fi))
                        {
                            //get the first worksheet in the workbook
                            var worksheet = package.Workbook.Worksheets[0];
                            int colCount = worksheet.Dimension.End.Column; //get Column Count
                            int rowCount = worksheet.Dimension.End.Row; //get row count
                                                                        // var currentViewId = null;

                            for (int col = 1; col <= colCount; col++)
                            {
                                for (int row = 1; row <= rowCount; row++)
                                {
                                    if (col == 1)
                                    {
                                        var currentValue = worksheet.Cells[row, col].Value?.ToString();

                                        if (string.IsNullOrEmpty(currentValue) || headers.Contains(currentValue))
                                        {

                                        }
                                        else
                                        {
                                            if (!employees.Contains(currentValue))
                                            {
                                                var description = worksheet.Cells[row, col + 1].Value?.ToString().Trim();
                                                var categoryId = int.Parse(worksheet.Cells[row, col + 2].Value?.ToString());
                                                var imageUrl = worksheet.Cells[row, col + 3].Value?.ToString();
                                                var isViewExists = repo.Get().Any(r => r.Name == currentValue);
                                                employees.Add(currentValue);
                                                if (!isViewExists)
                                                {
                                                    var employee = new Employee() { Name = currentValue, Description = description, CategoryId = categoryId, ImageUrl = imageUrl };
                                                    repo.Add(employee);
                                                }
                                                else
                                                {
                                                    var employee = repo.Get().FirstOrDefault(r => r.Name == currentValue);
                                                    if (employee != null)
                                                    {
                                                        employee.Description = description;
                                                        employee.CategoryId = categoryId;
                                                        if (string.IsNullOrEmpty(employee.ImageUrl))
                                                            employee.ImageUrl = imageUrl;
                                                        isSaved= repo.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("UploadExcel", "برجاء تحميل الملف ");
                    return View(model);
                }
                if (isSaved)
                {
                    ViewBag.isSaved = "تم المفظ بنجاح";
                }
                else
                {
                    ViewBag.isSaved = "لا يوجد تغيير";
                }
                return View("UploadExcelData");
            }
            catch (Exception ex)
            {
                ViewBag.isSaved = "لم يتم الحفظ برجاء المحاولة مرة اخرى "+ex.Message;
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeViewModel()
            {
                Categories = SelectDdlDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = string.Empty;
                    if (model.Url != null)
                    {
                        string url = Path.Combine(env.ContentRootPath, "wwwRoot/uploads");
                        fileName = model.Url.FileName;
                        string fullPath = Path.Combine(url, fileName);
                        model.Url.CopyToAsync(new FileStream(fullPath, FileMode.Create));
                    }
                    if (model.CategoryId > 0)
                    {
                        repo.Add(new Employee()
                        {
                            Description = model.EmployeeDescription,
                            Name = model.EmployeeName,
                            Category = CategoryRepo.GetById(model.CategoryId),
                            ImageUrl = model.Url.FileName
                        });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorId = "برجاء ادخال التصنيف";
                        model = new EmployeeViewModel()
                        {
                            Categories = SelectDdlDefault()
                        };
                        return View(model);
                    }
                }
                else
                {
                    model = new EmployeeViewModel()
                    {
                        Categories = SelectDdlDefault()
                    };
                    ModelState.AddModelError("", "برجاء ادخال جميع البيانات");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patinet = repo.GetById(id);
            if (patinet.Category.Id > 0)
            {
                var Category = CategoryRepo.GetById(patinet.Category.Id);
                var employeeViewModel = new EmployeeViewModel()
                {
                    EmployeeId = patinet.Id,
                    EmployeeDescription = patinet.Description,
                    EmployeeName = patinet.Name,
                    EmployeeTitle = patinet.Category.Name,
                    CategoryId = Category.Id,
                    Categories = SelectDdlDefault(),
                    ImageUrl = patinet.ImageUrl
                };
                return View(employeeViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (employeeViewModel.CategoryId > 0)
                {
                    var currentEmployee = repo.GetById(employeeViewModel.EmployeeId); ;
                    string fileName = string.Empty;
                    //  if (patientViewModel.Url != null)
                    //   {
                    string url = Path.Combine(env.ContentRootPath, "wwwRoot/uploads");
                    if (employeeViewModel.Url != null)
                    {
                        fileName = employeeViewModel.Url.FileName;
                        string fullPath = Path.Combine(url, fileName);
                        string oldFileName = repo.GetById(employeeViewModel.EmployeeId).ImageUrl;
                        string fullOldPath = Path.Combine(url, oldFileName);
                        // if (fullPath != fullOldPath)
                        //{
                        // System.IO.File.Delete(fullOldPath);
                        employeeViewModel.Url.CopyTo(new FileStream(fullPath, FileMode.Create));
                        //}
                    }
                    else
                    {
                        fileName = currentEmployee.ImageUrl;
                    }

                    repo.Update(employeeViewModel.EmployeeId, new Employee()
                    {
                        Name = employeeViewModel.EmployeeName,
                        Description = employeeViewModel.EmployeeDescription,
                        CategoryId = employeeViewModel.CategoryId,
                        ImageUrl = fileName,
                    });
                    return RedirectToAction("Index");
                    //}
                    //else
                    //{
                    //    patientViewModel = new PatientViewModel()
                    //    {
                    //        Categories = SelectDdlDefault()
                    //    };
                    //    ModelState.AddModelError("", "Fill All Fields");
                    //    return View(patientViewModel);
                    //}
                }
                else
                {
                    ViewBag.ErrorId = "برجاء ادخال التصنيف";
                    employeeViewModel = new EmployeeViewModel()
                    {
                        Categories = SelectDdlDefault()
                    };
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public List<Category> SelectDdlDefault()
        {
            var Categorys = CategoryRepo.Get().ToList();
            Categorys.Insert(0, new Category() { Id = -1, Name = "---- اختر التصنيف -------" });
            return Categorys;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var patinet = repo.GetById(id);
            return View(patinet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                repo.Delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}
