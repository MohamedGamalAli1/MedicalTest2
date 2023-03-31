using AspNetCore.Reporting;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    public class ArchieveController : Controller
    {
        private readonly IStoreRepo<Employee> repo;
        //private readonly IStoreRepo<Category> CategoryRepo;
        //private readonly IStoreRepo<EmployeeCategory> empoyeeCatRepo;
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private static List<Employee> employees;
        private readonly IStoreRepo<Nationality> nationalityRepo;
        private readonly IStoreRepo<Destination> destinationRepo;
        private readonly IStoreRepo<Department> departmentRepo;
        private readonly IStoreRepo<ActualWork> actualWorkRepo;
        private readonly IStoreRepo<JobCategory> jobCategoryRepo;
        //private readonly IStoreRepo<Category> categoryRepo;
        //public ArchieveController(IWebHostEnvironment _webHostEnvirnoment, IStoreRepo<Employee> repo, IStoreRepo<Category> CategoryRepo, IStoreRepo<EmployeeCategory> empoyeeCatRepo)
        //{
        //    this.repo = repo;
        //    this.CategoryRepo = CategoryRepo;
        //    this.empoyeeCatRepo = empoyeeCatRepo;
        //    this._webHostEnvirnoment = _webHostEnvirnoment;
        //    // employees = new List<Employee>();
        //}

        public ArchieveController(IWebHostEnvironment _webHostEnvirnoment, IStoreRepo<Employee> repo, IStoreRepo<Nationality> nationalityRepo, IStoreRepo<Destination> destinationRepo, IStoreRepo<Department> departmentRepo, IStoreRepo<ActualWork> actualWorkRepo, IStoreRepo<JobCategory> jobCategoryRepo)
        {
            this.repo = repo;
            this.nationalityRepo = nationalityRepo;
            this.destinationRepo = destinationRepo;
            this.departmentRepo = departmentRepo;
            this.actualWorkRepo = actualWorkRepo;
            this.jobCategoryRepo = jobCategoryRepo;
            this._webHostEnvirnoment = _webHostEnvirnoment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(ArchieveViewModel model, int pageindex = 1,
                                      string sortExpression = "Name")
        {
            var query = repo.GetQueryable();
            if (model.DepartmentId > 0)
            {
                query = query.Where((a => a.DepartmentId == model.DepartmentId));
            }
            if (model.ActualWorkId > 0)
            {
                query = query.Where((a => a.ActualWorkId == model.ActualWorkId));
            }
            if (model.JobCategoryId > 0)
            {
                query = query.Where((a => a.JobCategoryId == model.JobCategoryId));
            }
            if (!string.IsNullOrWhiteSpace(model.NationalId))
            {
                query = query.Where((a => a.NationalId == model.NationalId));
            }
            if (model.ComputerNumber > 0)
            {
                query = query.Where((a => a.ComputerNumber == model.ComputerNumber));
            }

            query = query.Where((a => a.IsRetired == model.IsRetired));

            var res = query.Select(r => new EmployeeDataVM
            {
                Id = r.Id,
                CreationDate = r.CreationDate.ToString("dd/MM/yyyy"),
                Name = r.FirstName + " " + r.SecondName + " " + (!string.IsNullOrWhiteSpace(r.FamilyName) ? r.FamilyName : ""),
                NationalId = r.NationalId,
                DestinationName = r.Destination.Name,
                DepartmentName = r.Department.Name,
                ComputerNumber = r.ComputerNumber,
                JobCategoryName = r.JobCategory.Name,
                ActualWorkName = r.ActualWork.Name,
                WorkTypeName = r.WorkType.Name,
                PhoneNumber = r.PhoneNumber,
                Email = r.Email,
                // Email = dbContext.Users.FirstOrDefault(x => x.EmployeeId == r.Id).Email,
                IsRetired = r.IsRetired,
            }).AsQueryable();
            //if (!string.IsNullOrWhiteSpace(filter))
            //{
            //    qry = qry.Where(p => p.PhoneNumber.Contains(filter));
            //}

            var result = await PagingList.CreateAsync(
                                         res, 5, pageindex, sortExpression, "Name");


            var archieve = new ArchieveViewModel()
            {
                EmployeesData = result,
                Departments = SelectDdlDepartment(),
                ActualWorks = SelectDdlActualWork(),
                JobCategories = SelectDdlJobCategory()
            };
            if (query.Count() > 0)
            {
                return View(archieve);
            }
            else
            {
                var archieveViewModel = new ArchieveViewModel()
                {
                    EmployeesData = result,
                    Departments = SelectDdlDepartment(),
                    ActualWorks = SelectDdlActualWork(),
                    JobCategories = SelectDdlJobCategory()
                };
                ViewBag.archieveViewModelError = "لايوجد بيانات ";
                return View(archieveViewModel);
            }
        }
        public IActionResult Print()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report2.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("DateFrom", DateTime.Now.ToString());
            //get products from product table 
            var patientList = employees;
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", patientList);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }
        [HttpGet]
        public IActionResult ViewArchieve()
        {
            var archieveViewModel = new ArchieveViewModel()
            {
                //Nationalities = SelectDdlNationality(),
                //Destinations = SelectDdlDestination(),
                Departments = SelectDdlDepartment(),
                ActualWorks = SelectDdlActualWork(),
                JobCategories = SelectDdlJobCategory()
            };

            return View(archieveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ViewArchieve(ArchieveViewModel model, int pageindex = 1,
                                      string sortExpression = "Name")
        {
            var query = repo.GetQueryable();
            if (model.DepartmentId > 0)
            {
                query = query.Where((a => a.DepartmentId == model.DepartmentId));
            }
            if (model.ActualWorkId > 0)
            {
                query = query.Where((a => a.ActualWorkId == model.ActualWorkId));
            }
            if (model.JobCategoryId > 0)
            {
                query = query.Where((a => a.JobCategoryId == model.JobCategoryId));
            }
            if (!string.IsNullOrWhiteSpace(model.NationalId))
            {
                query = query.Where((a => a.NationalId == model.NationalId));
            }
            if (model.ComputerNumber > 0)
            {
                query = query.Where((a => a.ComputerNumber == model.ComputerNumber));
            }

            query = query.Where((a => a.IsRetired == model.IsRetired));

            var res = query.Select(r => new EmployeeDataVM
            {
                Id = r.Id,
                CreationDate = r.CreationDate.ToString("dd/MM/yyyy"),
                Name = r.FirstName + " " + r.SecondName + " " + (!string.IsNullOrWhiteSpace(r.FamilyName) ? r.FamilyName : ""),
                NationalId = r.NationalId,
                DestinationName = r.Destination.Name,
                DepartmentName = r.Department.Name,
                ComputerNumber = r.ComputerNumber,
                JobCategoryName = r.JobCategory.Name,
                ActualWorkName = r.ActualWork.Name,
                WorkTypeName = r.WorkType.Name,
                PhoneNumber = r.PhoneNumber,
                Email = r.Email,
                // Email = dbContext.Users.FirstOrDefault(x => x.EmployeeId == r.Id).Email,
                IsRetired = r.IsRetired,
            }).AsQueryable();
            //if (!string.IsNullOrWhiteSpace(filter))
            //{
            //    qry = qry.Where(p => p.PhoneNumber.Contains(filter));
            //}

            var result = await PagingList.CreateAsync(
                                         res, 5, pageindex, sortExpression, "Name");

            //result.RouteValue = new RouteValueDictionary { { "filter", filter } };
            //return View(result);
            //var query = repo.GetQueryable();
            //if (model.CatId > 0)
            //{
            //    query = query.Where((a => a.JobCategory.Id == model.CatId));
            //}
            //if (model.EmployeeCatId > 0)
            //{
            //    query = query.Where((a => a.EmployeeCategory.Id == model.EmployeeCatId));
            //}
            //if (!string.IsNullOrWhiteSpace(archieveViewModel.NationalId))
            //{
            //    query = query.Where(a => a.NationalId == model.NationalId);
            //}
            //if (model.From.HasValue)
            //{
            //    query = query.Where(a => a.ArchieveDate >= model.From);
            //}
            //if (model.To.HasValue)
            //{
            //    query = query.Where(a => a.ArchieveDate <= model.To);
            //}
            //employees = query.Select(model => new Employee
            //{
            //    //Id = e.Id,
            //    //Description = e.Description,
            //    //CategoryId = e.CategoryId,
            //    //ArchieveDate = e.ArchieveDate,
            //    //ImageUrl = e.ImageUrl,
            //    //Name = e.Name,

            //    Id = model.Id,

            //    FirstName = model.FirstName,
            //    SecondName = model.SecondName,
            //    FamilyName = model.FamilyName,
            //    //Nationality = nationalityRepo.GetById(model.NationalityId),
            //    //Destination = destinationRepo.GetById(model.DestinationId),
            //    //Department = departmentRepo.GetById(model.DepartmentId),
            //    //ActualWork = actualWorkRepo.GetById(model.ActualWorkId),
            //    //JobCategory = jobCategoryRepo.GetById(model.JobCategoryId),
            //    //EmployeeCategory = empoyeeCatRepo.GetById(model.EmployeeCategoryId),
            //    NationalityId = model.NationalityId,
            //    DestinationId = model.DestinationId,
            //    DepartmentId = model.DepartmentId,
            //    ActualWorkId = model.ActualWorkId,
            //    JobCategoryId = model.JobCategoryId,
            //    NationalId = model.NationalId,
            //    Gender = model.Gender,
            //    IslamicDate = model.IslamicDate,
            //    BitrhDate = model.BitrhDate,
            //    CreationDate = DateTime.UtcNow,
            //    ComputerNumber = model.ComputerNumber,
            //    WorkType = model.WorkType,
            //    PhoneNumber = model.PhoneNumber,
            //    //Email = model.Email,
            //}).ToList();
            var archieve = new ArchieveViewModel()
            {
                EmployeesData = result,
                //Nationalities = SelectDdlNationality(),
                //Destinations = SelectDdlDestination(),
                Departments = SelectDdlDepartment(),
                ActualWorks = SelectDdlActualWork(),
                JobCategories = SelectDdlJobCategory()
            };
            if (query.Count() > 0)
            {
                return View(archieve);
            }
            else
            {
                var archieveViewModel = new ArchieveViewModel()
                {
                    EmployeesData = result,
                    //Nationalities = SelectDdlNationality(),
                    //Destinations = SelectDdlDestination(),
                    Departments = SelectDdlDepartment(),
                    ActualWorks = SelectDdlActualWork(),
                    JobCategories = SelectDdlJobCategory()
                };
                ViewBag.archieveViewModelError = "لايوجد بيانات ";
                return View(archieveViewModel);
            }
        }
        public List<Nationality> SelectDdlNationality()
        {
            var result = nationalityRepo.Get().ToList();
            result.Insert(0, new Nationality() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<Destination> SelectDdlDestination()
        {
            var result = destinationRepo.Get().ToList();
            result.Insert(0, new Destination() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<Department> SelectDdlDepartment()
        {
            var result = departmentRepo.Get().ToList();
            result.Insert(0, new Department() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<ActualWork> SelectDdlActualWork()
        {
            var result = actualWorkRepo.Get().ToList();
            result.Insert(0, new ActualWork() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<JobCategory> SelectDdlJobCategory()
        {
            var result = jobCategoryRepo.Get().ToList();
            result.Insert(0, new JobCategory() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
    }
}
