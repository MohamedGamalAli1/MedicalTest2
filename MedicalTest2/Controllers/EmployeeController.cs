using MedicalTest2.Data;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using ReflectionIT.Mvc.Paging;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace MedicalTest2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IStoreRepo<Employee> repo;
        private readonly IStoreRepo<Nationality> nationalityRepo;
        private readonly IStoreRepo<Destination> destinationRepo;
        private readonly IStoreRepo<Department> departmentRepo;
        private readonly IStoreRepo<ActualWork> actualWorkRepo;
        private readonly IStoreRepo<JobCategory> jobCategoryRepo;
        private readonly IStoreRepo<Gender> genderRepo;
        private readonly IStoreRepo<WorkType> workTypeRepo;
        private readonly IStoreRepo<Attachment> attachmentRepo;
        private readonly IStoreRepo<EmployeeAttachment> employeeAttachmentRepo;

        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        [BindProperty]
        public EmployeeViewModel Input { get; set; }

        private readonly IHostEnvironment env;
        private readonly List<string> permittedExcelExtensions;
        public EmployeeController(
            UserManager<MyUser> userManager,
            SignInManager<MyUser> signInManager,
            IStoreRepo<Employee> repo, IStoreRepo<Nationality> nationalityRepo,
            IStoreRepo<Destination> destinationRepo, IStoreRepo<Department> departmentRepo,
            IStoreRepo<ActualWork> actualWorkRepo, IStoreRepo<JobCategory> jobCategoryRepo,
            IHostEnvironment env, IStoreRepo<Gender> genderRepo,
            IStoreRepo<WorkType> workTypeRepo,
            IStoreRepo<Attachment> attachmentRepo,
            IStoreRepo<EmployeeAttachment> employeeAttachmentRepo
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.repo = repo;
            this.nationalityRepo = nationalityRepo;
            this.destinationRepo = destinationRepo;
            this.departmentRepo = departmentRepo;
            this.actualWorkRepo = actualWorkRepo;
            this.jobCategoryRepo = jobCategoryRepo;
            this.genderRepo = genderRepo;
            this.workTypeRepo = workTypeRepo;
            this.env = env;
            permittedExcelExtensions = new List<string>() { ".xlsx", ".xls" };
            this.attachmentRepo = attachmentRepo;
            this.employeeAttachmentRepo = employeeAttachmentRepo;
        }
        [BindProperty]
        public string M { get; set; }
        //[HttpGet]
        //public IActionResult Index(int pageindex = 1)
        //{
        //    var employees = repo.Get();
        //    var model = PagingList.Create(employees, 5, pageindex);
        //    return View(model);
        //}
        //public ActionResult CreatePDFDocument()
        //{
        //    //Generate a new PDF document.
        //    PdfDocument doc = new PdfDocument();
        //    //Add a page.
        //    PdfPage page = doc.Pages.Add();
        //    //Create a PdfGrid.
        //    PdfGrid pdfGrid = new PdfGrid();
        //    //Add values to list
        //    List<object> data = new List<object>();
        //    Object row1 = new { ID = "E01", Name = "Clay" };
        //    Object row2 = new { ID = "E02", Name = "Thomas" };
        //    Object row3 = new { ID = "E03", Name = "Andrew" };
        //    Object row4 = new { ID = "E04", Name = "Paul" };
        //    Object row5 = new { ID = "E05", Name = "Gray" };
        //    data.Add(row1);
        //    data.Add(row2);
        //    data.Add(row3);
        //    data.Add(row4);
        //    data.Add(row5);
        //    //Add list to IEnumerable
        //    IEnumerable<object> dataTable = data;
        //    //Assign data source.
        //    pdfGrid.DataSource = dataTable;
        //    //Draw grid to the page of PDF document.
        //    pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
        //    //Write the PDF document to stream
        //    MemoryStream stream = new MemoryStream();
        //    doc.Save(stream);
        //    //If the position is not set to '0' then the PDF will be empty.
        //    stream.Position = 0;
        //    //Close the document.
        //    doc.Close(true);
        //    //Defining the ContentType for pdf file.
        //    string contentType = "application/pdf";
        //    //Define the file name.
        //    string fileName = "Output.pdf";
        //    //Creates a FileContentResult object by using the file contents, content type, and file name.
        //    return File(stream, contentType, fileName);
        //}
        public async Task<IActionResult> Index(string filter, int pageindex = 1,
                                       string sortExpression = "Name")
        {


            int t = 0;
            var qry = repo.GetQueryable().Select(r => new EmployeeDataVM
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
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Name.Contains(filter) || p.PhoneNumber.Contains(filter) || p.NationalId.Contains(filter)
                || p.ComputerNumber == (int.TryParse(filter, out t) == true ? t : 0));
            }
            //if (!string.IsNullOrWhiteSpace(filter))
            //{
            //    qry = qry.Where(p => p.PhoneNumber.Contains(filter));
            //}

            var model = await PagingList.CreateAsync(
                                         qry, 10, pageindex, sortExpression, "Name");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }
        //public IActionResult Index(int? page)//Add page parameter
        //{
        //    var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
        //    int pageSize = 5; // Get 25 employess for each requested page.
        //    var onePageOfEmployees = repo.Get().ToPagedList(pageNumber, pageSize);
        //    return View(onePageOfEmployees); // Send 25 Employees to the page.
        //}


        public IActionResult UploadExcelData()
        {

            // var fi = new FileInfo(@"excel file.xlsx");
            UploadExcelModel model = new UploadExcelModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadExcelData(UploadExcelModel model)
        {
            try
            {
                ViewBag.isSaved = "";
                var isSaved = false;
                //if (ModelState.IsValid)
                //{
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
                    model.Url.CopyTo(new FileStream(fi, FileMode.Create));
                    //   string fi = Path.Combine(env.ContentRootPath, "wwwRoot/excelFile.xlsx");
                    //var fi = new FileInfo(@"C:\Users\moham\source\repos\ReadExcel\ReadExcel\excelFile.xlsx");

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    List<string> headers = new List<string>() { "FirstName","SecondName","FamilyName","Gender","CreationDate","IsRetired",
                            "Nationality","BitrhDate", "NationalId", "PhoneNumber",
                            "ActualWork", "ComputerNumber", "Password", "ConfirmPassword", "Department", "Destination", "IslamicDate", "JobCategory" };
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
                                            var genderId = 1;
                                            var nationalityId = 1;
                                            var actualWorkId = 1;
                                            var departmentId = 1;
                                            var destinationId = 1;
                                            var jobCategoryId = 1;
                                            var workTypeId = 1;
                                            var secondName = worksheet.Cells[row, col + 1].Value?.ToString().Trim();
                                            var familyName = worksheet.Cells[row, col + 2].Value?.ToString();
                                            var gender = worksheet.Cells[row, col + 3].Value?.ToString();
                                            // check gender if exists
                                            var isExistsGender = genderRepo.GetByName(gender);
                                            if (isExistsGender != null)
                                            {
                                                genderId = isExistsGender.Id;
                                            }
                                            var creationDate = worksheet.Cells[row, col + 4].Value?.ToString();
                                            var workType = worksheet.Cells[row, col + 5].Value?.ToString();
                                            var isExistsWorkType = workTypeRepo.GetByName(workType);
                                            if (isExistsWorkType != null)
                                            {
                                                workTypeId = isExistsWorkType.Id;
                                            }
                                            var isRetired = worksheet.Cells[row, col + 6].Value.ToString();
                                            var nationality = worksheet.Cells[row, col + 7].Value?.ToString();
                                            // check nationality if exists
                                            var isExistsNationality = nationalityRepo.GetByName(nationality);
                                            if (isExistsNationality != null)
                                            {
                                                nationalityId = isExistsNationality.Id;
                                            }
                                            var bitrhDate = worksheet.Cells[row, col + 8].Value?.ToString();

                                            var nationalId = worksheet.Cells[row, col + 9].Value?.ToString();
                                            var phoneNumber = worksheet.Cells[row, col + 10].Value?.ToString();
                                            var actualWork = worksheet.Cells[row, col + 11].Value?.ToString();
                                            // check actualWork if exists
                                            var isExistsActualWork = actualWorkRepo.GetByName(actualWork);
                                            if (isExistsActualWork != null)
                                            {
                                                actualWorkId = isExistsActualWork.Id;
                                            }
                                            var computerNumber = int.Parse(worksheet.Cells[row, col + 12].Value?.ToString());
                                            var password = worksheet.Cells[row, col + 13].Value?.ToString();
                                            var confirmPassword = worksheet.Cells[row, col + 14].Value?.ToString();
                                            var department = worksheet.Cells[row, col + 15].Value?.ToString();
                                            var isExistsDepartment = departmentRepo.GetByName(department);
                                            if (isExistsDepartment != null)
                                            {
                                                departmentId = isExistsDepartment.Id;
                                            }
                                            // check department if exists
                                            var destination = worksheet.Cells[row, col + 16].Value?.ToString();
                                            // check destination if exists
                                            var isExistsDestination = destinationRepo.GetByName(destination);
                                            if (isExistsDestination != null)
                                            {
                                                destinationId = isExistsDestination.Id;
                                            }
                                            var email = worksheet.Cells[row, col + 17].Value?.ToString();
                                            //var islamicDate = worksheet.Cells[row, col + 18].Value?.ToString();
                                            var jobCategory = worksheet.Cells[row, col + 18].Value?.ToString();
                                            // check jobCategory if exists
                                            var isExistsJobCategoryn = jobCategoryRepo.GetByName(jobCategory);
                                            if (isExistsJobCategoryn != null)
                                            {
                                                jobCategoryId = isExistsJobCategoryn.Id;
                                            }
                                            var isViewExists = repo.Get().Any(r => r.FirstName == currentValue && r.SecondName == secondName);
                                            employees.Add(currentValue);
                                            if (!isViewExists)
                                            {
                                                var currentUser = new Employee()
                                                {
                                                    FirstName = currentValue,
                                                    SecondName = secondName,
                                                    FamilyName = familyName,
                                                    Nationality = nationalityRepo.GetById(nationalityId),
                                                    Destination = destinationRepo.GetById(destinationId),
                                                    Department = departmentRepo.GetById(departmentId),
                                                    ActualWork = actualWorkRepo.GetById(actualWorkId),
                                                    JobCategory = jobCategoryRepo.GetById(jobCategoryId),
                                                    NationalId = nationalId,
                                                    Gender = genderRepo.GetById(genderId),
                                                    //IslamicDate = Convert.ToDateTime(islamicDate),
                                                    BitrhDate = Convert.ToDateTime(bitrhDate),
                                                    ComputerNumber = computerNumber,
                                                    WorkType = workTypeRepo.GetById(workTypeId),
                                                    PhoneNumber = phoneNumber,
                                                    Email = email,
                                                    CreationDate = Convert.ToDateTime(creationDate),
                                                    //Password = password,
                                                    //ConfirmPassword = confirmPassword,
                                                    IsRetired = false
                                                };
                                                repo.Add(currentUser);

                                                var user = new MyUser { Name = currentUser.FirstName, UserName = currentUser.FirstName.Trim(), Email = email, Password = password, EmployeeId = currentUser.Id };

                                                var result = await _userManager.CreateAsync(user, user.Password);
                                                if (result.Succeeded)
                                                {
                                                    await _userManager.AddToRoleAsync(user, "User");
                                                }
                                                foreach (var error in result.Errors)
                                                {
                                                    ModelState.AddModelError(string.Empty, error.Description);
                                                }
                                            }
                                            else
                                            {
                                                var employee = repo.Get().FirstOrDefault(r => r.FirstName == currentValue && r.SecondName == secondName);

                                                if (employee != null)
                                                {
                                                    //employee.Name = currentValue;
                                                    //employee.Description = description;
                                                    //employee.CategoryId = categoryId;
                                                    //employee.ArchieveDate = Convert.ToDateTime(archieveDate);
                                                    //employee.EmployeeCategoryId = (int)employeeCategoryId;
                                                    //employee.BitrhDate = Convert.ToDateTime(bitrhDate);
                                                    //employee.NationalId = nationalId;
                                                    //employee.PhoneNumber = phoneNumber;
                                                    employee.FirstName = currentValue;
                                                    employee.SecondName = secondName;
                                                    employee.FamilyName = familyName;
                                                    employee.Nationality = nationalityRepo.GetById(nationalityId);
                                                    employee.Destination = destinationRepo.GetById(destinationId);
                                                    employee.Department = departmentRepo.GetById(departmentId);
                                                    employee.ActualWork = actualWorkRepo.GetById(actualWorkId);
                                                    employee.JobCategory = jobCategoryRepo.GetById(jobCategoryId);
                                                    employee.NationalId = nationalId;
                                                    employee.Gender = genderRepo.GetById(genderId);
                                                    //employee.IslamicDate = Convert.ToDateTime(islamicDate);
                                                    employee.BitrhDate = Convert.ToDateTime(bitrhDate);
                                                    employee.ComputerNumber = computerNumber;
                                                    employee.WorkType = workTypeRepo.GetById(workTypeId);
                                                    employee.PhoneNumber = phoneNumber;
                                                    employee.Email = email;
                                                    employee.CreationDate = Convert.ToDateTime(creationDate);
                                                    //employee.Password = password;
                                                    //employee.ConfirmPassword = confirmPassword;
                                                    isSaved = repo.SaveChanges();
                                                }
                                            }
                                            ViewBag.isSaved = "تم الحفظ بنجاح";
                                            //model.Url.FileName="";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //}
                //else
                //{
                //    ModelState.AddModelError("UploadExcel", "برجاء تحميل الملف ");
                //    return View(model);
                //}
                //if (isSaved)
                //{
                //    ViewBag.isSaved = "تم المفظ بنجاح";
                //}
                //else
                //{
                //    ViewBag.isSaved = "لا يوجد تغيير";
                //}
                //return View("UploadExcelData");
                return RedirectToAction("UploadExcelData");
            }
            catch (Exception ex)
            {
                ViewBag.isSaved = "لم يتم الحفظ برجاء المحاولة مرة اخرى " + ex.Message;
                return View(ex);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            /*
              nationalityRepo
              destinationRepo
             departmentRepo
              actualWorkRepo
              jobCategoryRepo
             */

            ViewBag.NoDataError = "";
            if (!nationalityRepo.Get().Any() || !nationalityRepo.Get().Any() || !departmentRepo.Get().Any() || !actualWorkRepo.Get().Any() || !jobCategoryRepo.Get().Any())
            {
                ViewBag.NoDataError = " برجاء اضافة التصنيفات  اولا";
            }
            else
            {
                ViewBag.NoDataError = "";
            }
            var model = RefreshViewModel();
            return View(model);
        }
        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            return (DateConv.Date.ToString("f", DTFormat));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string gender, [FromForm] string WorkType, EmployeeViewModel model)
        {
            try
            {
                var selectedGenderId = 1;
                var selectedWorkTypeId = 1;
                var genderResult = genderRepo.GetByName(gender);
                selectedGenderId = genderResult.Id;
                var workTypeResult = workTypeRepo.GetByName(WorkType);
                selectedWorkTypeId = workTypeResult.Id;
                //switch (gender)
                //{
                //    case "ذكر":
                //        selectedGenderId = 1;
                //        break;
                //    case "أنثى":
                //        selectedGenderId = 2;

                //        break;
                //}
                //switch (WorkType)
                //{
                //    case "وزارة":
                //        selectedWorkTypeId = 1;
                //        break;
                //    case "برنامج":
                //        selectedWorkTypeId = 2;

                //        break;
                //    case "شركه":
                //        selectedWorkTypeId = 3;

                //        break;
                //}
                if (ModelState.IsValid)
                {

                    //string date = ConvertDateCalendar(DateTime.Now, "Hijri", "en-US");
                    //model.IslamicDate= ConvertDateCalendar(DateTime.Now, "Hijri", "en-US");

                    //if (model.NationalityId <= 0)
                    //{
                    //    ViewBag.NationalityErrorId = "برجاء ادخال الجنسية";
                    //    model = RefreshViewModel();
                    //    return View(model);
                    //}
                    //if (model.DestinationId <= 0)
                    //{
                    //    ViewBag.DestinationErrorId = "برجاء ادخال الجهة";
                    //    model = RefreshViewModel();
                    //    return View(model);
                    //}
                    //if (model.DepartmentId <= 0)
                    //{
                    //    ViewBag.DepartmentErrorId = "برجاء ادخال القسم";
                    //    model = RefreshViewModel();
                    //    return View(model);
                    //}
                    //if (model.ActualWorkId <= 0)
                    //{
                    //    ViewBag.ActualWorkErrorId = "برجاء ادخال العمل الفعلى";
                    //    model = RefreshViewModel();
                    //    return View(model);
                    //}
                    //if (model.JobCategoryId <= 0)
                    //{
                    //    ViewBag.JobCategoryErrorId = "برجاء ادخال فئة الوظيفة";
                    //    model = RefreshViewModel();
                    //    return View(model);
                    //}
                    var isExistsName = repo.Get().Any(r => r.FirstName == model.FirstName && r.SecondName == model.SecondName);
                    if (isExistsName)
                    {
                        model = RefreshViewModel();
                        ModelState.AddModelError("", "الاسم موجود من قبل");
                        return View(model);
                    }
                    var isExistsPhoneNumber = repo.Get().Any(r => r.PhoneNumber == model.PhoneNumber);
                    if (isExistsPhoneNumber)
                    {
                        model = RefreshViewModel();
                        ModelState.AddModelError("", "رقم الجوال موجود من قبل");
                        return View(model);
                    }
                    var isExistsComputerNumber = repo.Get().Any(r => r.ComputerNumber == model.ComputerNumber);
                    if (isExistsComputerNumber)
                    {
                        model = RefreshViewModel();
                        ModelState.AddModelError("", "رقم الحاسب موجود من قبل");
                        return View(model);
                    }
                    var isExistsNationalityId = repo.Get().Any(r => r.NationalId == model.NationalId);
                    if (isExistsNationalityId)
                    {
                        model = RefreshViewModel();
                        ModelState.AddModelError("", "رقم الهوية موجود من قبل");
                        return View(model);
                    }
                    var isEmailAllowed = model.Email.EndsWith("@moh.gov.sa");
                    if (!isEmailAllowed)
                    {
                        model = RefreshViewModel();
                        ModelState.AddModelError("", "برجاء الايميل ينتهى @moh.gov.sa");
                        return View(model);
                    }
                    var currentUser = new Employee()
                    {
                        FirstName = model.FirstName,
                        SecondName = model.SecondName,
                        FamilyName = model.FamilyName,
                        Nationality = nationalityRepo.GetById(model.NationalityId),
                        Destination = destinationRepo.GetById(model.DestinationId),
                        Department = departmentRepo.GetById(model.DepartmentId),
                        ActualWork = actualWorkRepo.GetById(model.ActualWorkId),
                        JobCategory = jobCategoryRepo.GetById(model.JobCategoryId),
                        //EmployeeCategory = empoyeeCatRepo.GetById(model.EmployeeCategoryId),
                        NationalId = model.NationalId,
                        Gender = genderRepo.GetById(selectedGenderId),
                        //IslamicDate = model.IslamicDate,
                        BitrhDate = model.BitrhDate,
                        CreationDate = DateTime.UtcNow,
                        ComputerNumber = model.ComputerNumber,
                        WorkType = workTypeRepo.GetById(selectedWorkTypeId),
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        //Password = model.Password,
                        //ConfirmPassword = model.ConfirmPassword,
                        IsRetired = false
                    };
                    repo.Add(currentUser);

                    model.ReturnUrl ??= Url.Content("~/");
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    if (ModelState.IsValid)
                    {
                        var user = new MyUser { Name = currentUser.FirstName, UserName = currentUser.FirstName.Trim(), Email = model.Email, Password = model.Password, EmployeeId = currentUser.Id };

                        var result = await _userManager.CreateAsync(user, user.Password);
                        if (result.Succeeded)
                        {
                            //_logger.LogInformation("User created a new account with password.");
                            foreach (var employeeAttach in model.EmployeeAttachments)
                            {
                                employeeAttach.EmployeeId = currentUser.Id;
                                employeeAttachmentRepo.Add(employeeAttach);
                            }
                            await _userManager.AddToRoleAsync(user, "User");

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                                protocol: Request.Scheme);

                            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                            if (_userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = model.ReturnUrl });
                            }
                            else
                            {

                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(model.ReturnUrl);
                            }

                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    model = RefreshViewModel();
                    ModelState.AddModelError("", "برجاء ادخال جميع البيانات");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        private EmployeeViewModel RefreshViewModel()
        {
            return new EmployeeViewModel()
            {
                Nationalities = SelectDdlNationality(),
                Destinations = SelectDdlDestination(),
                Departments = SelectDdlDepartment(),
                ActualWorks = SelectDdlActualWork(),
                JobCategories = SelectDdlJobCategory(),
                Genders = SelectDdlGender(),
                WorkTypes = SelectDdlWorkType(),
                Attachments = SelectDdlAttachment(),



            };
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = repo.GetById(id);
            if (model.NationalityId > 0 && model.DestinationId > 0 && model.DepartmentId > 0 && model.ActualWorkId > 0 && model.JobCategoryId > 0)
            {
                /*
                            NationalId = model.NationalId,
                            Gender = model.Gender,
                            IslamicDate = model.IslamicDate,
                            BitrhDate = model.BitrhDate,
                            CreationDate = DateTime.UtcNow,
                            ComputerNumber = model.ComputerNumber,
                            WorkType = model.WorkType,
                            PhoneNumber = model.PhoneNumber,
                            Email = model.Email,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
  */
                var nationality = nationalityRepo.GetById(model.Nationality.Id);
                var destination = destinationRepo.GetById(model.Destination.Id);
                var department = departmentRepo.GetById(model.Department.Id);
                var actualWork = actualWorkRepo.GetById(model.ActualWork.Id);
                var jobCategory = jobCategoryRepo.GetById(model.JobCategory.Id);

                //var employeeCategory = empoyeeCatRepo.GetById(Employee.EmployeeCategory.Id);

                var employeeViewModel = new EmployeeViewModel()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    FamilyName = model.FamilyName,
                    NationalityId = nationality.Id,
                    DestinationId = destination.Id,
                    DepartmentId = department.Id,
                    ActualWorkId = actualWork.Id,
                    JobCategoryId = jobCategory.Id,
                    NationalId = model.NationalId,
                    GenderId = model.GenderId,
                    WorkTypeId = model.WorkTypeId,
                    //IslamicDate = model.IslamicDate,
                    BitrhDate = model.BitrhDate,
                    ComputerNumber = model.ComputerNumber,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    //Password = model.Password,
                    //CategoryId = category.Id,
                    //EmployeeCategoryId = employeeCategory.Id,
                    //Categories = SelectDdlDefault(),
                    //EmployeeCategories = SelectEmployeeCatDdlDefault(),
                    Nationalities = SelectDdlNationality(),
                    Destinations = SelectDdlDestination(),
                    Departments = SelectDdlDepartment(),
                    ActualWorks = SelectDdlActualWork(),
                    JobCategories = SelectDdlJobCategory(),
                    Genders = SelectDdlGender(model.GenderId),
                    WorkTypes = SelectDdlWorkType(model.WorkTypeId),
                    Attachments = SelectDdlAttachment(),
                    EmployeeAttachments = employeeAttachmentRepo.Get().Where(r => r.EmployeeId == id).ToList(),
                };
                return View(employeeViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if (model.NationalityId > 0 && model.DestinationId > 0 && model.DepartmentId > 0 && model.ActualWorkId > 0 && model.JobCategoryId > 0)
                {
                    var currentEmployee = repo.GetById(model.Id);

                    currentEmployee.FirstName = model.FirstName;
                    currentEmployee.SecondName = model.SecondName;
                    currentEmployee.FamilyName = model.FamilyName;
                    currentEmployee.NationalityId = model.NationalityId;
                    currentEmployee.DestinationId = model.DestinationId;
                    currentEmployee.DepartmentId = model.DepartmentId;
                    currentEmployee.ActualWorkId = model.ActualWorkId;
                    currentEmployee.JobCategoryId = model.JobCategoryId;
                    currentEmployee.NationalId = model.NationalId;
                    currentEmployee.GenderId = model.GenderId;
                    //IslamicDate = model.IslamicDate,
                    currentEmployee.BitrhDate = model.BitrhDate;
                    currentEmployee.ComputerNumber = model.ComputerNumber;
                    currentEmployee.WorkTypeId = model.WorkTypeId;
                    currentEmployee.PhoneNumber = model.PhoneNumber;
                    currentEmployee.Email = model.Email;
                    //Password = model.Password,
                    //CategoryId = category.Id,
                    //EmployeeCategoryId = employeeCategory.Id,
                    //Categories = SelectDdlDefault(),
                    //EmployeeCategories = SelectEmployeeCatDdlDefault(),
                    //Nationalities = SelectDdlNationality(),
                    //Destinations = SelectDdlDestination(),
                    //Departments = SelectDdlDepartment(),
                    //ActualWorks = SelectDdlActualWork(),
                    //JobCategories = SelectDdlJobCategory()



                    //   Nationality = nationalityRepo.GetById(nationalityId),

                    //IslamicDate = Convert.ToDateTime(islamicDate),

                    //Password = password,
                    //ConfirmPassword = confirmPassword,
                    repo.Update(model.Id, currentEmployee);

                    var employeeAttachments = employeeAttachmentRepo.Get();
                    var result = employeeAttachments.Where(r => r.EmployeeId == model.Id).ToList();
                    foreach (var item in result)
                    {
                        employeeAttachmentRepo.Delete(item);
                    }
                    
                    if(model.EmployeeAttachments!=null)
                    foreach (var employeeAttach in model.EmployeeAttachments)
                    {
                        employeeAttach.EmployeeId = model.Id;
                        employeeAttachmentRepo.Add(employeeAttach);
                    }
                    //foreach (var item in model.EmployeeAttachments)
                    //{
                    //    EmployeeAttachment employeeAttachment = new EmployeeAttachment();
                    //    employeeAttachment.EmployeeId = item.EmployeeId;
                    //    employeeAttachment.From = item.From;
                    //    employeeAttachment.To = item.To;
                    //    employeeAttachment.AttachmentId = item.AttachmentId;
                    //    employeeAttachmentRepo.Add(employeeAttachment);
                    //}

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
                    ViewBag.ErrorId = "برجاء ادخال القائمة";
                    model = RefreshViewModel();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        /*
          nationalityRepo
          destinationRepo
         departmentRepo
          actualWorkRepo
          jobCategoryRepo


        SelectDdlNationality
        SelectDdlDestination
        SelectDdlDepartment
        SelectDdlActualWork
        SelectDdlJobCategory


        Nationality
        Destination
        Department
        ActualWork
        JobCategory
         */
        public List<Nationality> SelectDdlNationality()
        {
            var result = nationalityRepo.Get().ToList();
            result.Insert(0, new Nationality() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<Attachment> SelectDdlAttachment()
        {
            var result = attachmentRepo.Get().ToList();
            result.Insert(0, new Attachment() { Id = -1, Name = "---- اختر مرفق -------" });
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
        public List<Gender> SelectDdlGender()
        {
            var result = genderRepo.Get().ToList();
            //result.Insert(0, new Gender() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<Gender> SelectDdlGender(int genderId)
        {
            var result = genderRepo.Get().ToList();
            foreach (var item in result)
            {
                if (item.Id == genderId)
                    item.IsSelected = true;
            }
            //result.Insert(0, new Gender() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<WorkType> SelectDdlWorkType()
        {
            var result = workTypeRepo.Get().ToList();
            //result.Insert(0, new Gender() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        public List<WorkType> SelectDdlWorkType(int workId)
        {
            var result = workTypeRepo.Get().ToList();
            foreach (var item in result)
            {
                if(item.Id== workId)
                item.IsSelected = true;
            }
            //result.Insert(0, new Gender() { Id = -1, Name = "---- اختر من القائمة -------" });
            return result;
        }
        //public List<Category> SelectDdlDefault()
        //{
        //    var Categorys = categoryRepo.Get().ToList();
        //    Categorys.Insert(0, new Category() { Id = -1, Name = "---- اختر التصنيف -------" });
        //    return Categorys;
        //}
        //public List<EmployeeCategory> SelectEmployeeCatDdlDefault()
        //{
        //    var employeeCategories = empoyeeCatRepo.Get().ToList();
        //    employeeCategories.Insert(0, new EmployeeCategory() { Id = -1, Name = "---- اختر التصنيف -------" });
        //    return employeeCategories;
        //}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Employee = repo.GetById(id);
            return View(Employee);
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
