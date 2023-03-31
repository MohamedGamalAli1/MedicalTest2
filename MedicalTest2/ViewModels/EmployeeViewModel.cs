using MedicalTest2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "الرقم")]
        public int Id { get; set; }
        [Required(ErrorMessage = "الاسم الاول مطلوب")]
        [Display(Name = " الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = " اسم الاب")]
        [Required(ErrorMessage = "اسم الاب مطلوب")]
        public string SecondName { get; set; }
        [Display(Name = " اسم العائلة")]
        public string FamilyName { get; set; }
        [Display(Name = "الجنسية")]
        //[Range(1, int.MaxValue)]

        //[Required(ErrorMessage = "الجنسية")]
        [Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار الجنسية")]
        public int NationalityId { get; set; }
        public List<Nationality> Nationalities { get; set; }
        [Display(Name = "رقم الهوية / الإقامة")]
        [Required(ErrorMessage = "رقم الهوية / الإقامة مطلوب")]
        public string NationalId { get; set; }
        //public GenderId Gender { get; set; }
        [BindProperty]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

       // [Display(Name = "التاريج الهجرى")]
     //   public DateTime? IslamicDate { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        [Required(ErrorMessage = "برجاء ادخال تاريخ الميلاد")]
        public DateTime? BitrhDate { get; set; }
        public DateTime CreationDate { get; set; }
        //public string CreatedBy { get; set; }
        //[Display(Name = "الحالة")]
        //public string IsRetired { get; set; }
        [Display(Name = "رقم الحاسب")]
        public int ComputerNumber { get; set; }
        [Display(Name = "الجهة")]
        [Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار الجهة")]
        public int DestinationId { get; set; }
        public List<Destination> Destinations { get; set; }

        [Display(Name = "القسم")]
        [Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار القسم")]
        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; }

        [Display(Name = "العمل الفعلى")]
        [Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار العمل الفعلى")]
        public int ActualWorkId { get; set; }
        public List<ActualWork> ActualWorks { get; set; }

        [Display(Name = "فئة الوظيفة")]
        [Required(ErrorMessage = "مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار فئة الوظيفة")]
        public int JobCategoryId { get; set; }
        public List<JobCategory> JobCategories { get; set; }

        [Display(Name = "نوع التعيبن")]
        [BindProperty]
        public int WorkTypeId { get; set; }
        public List<WorkType> WorkTypes { get; set; }
        //public int WorkType { get; set; }
        [Display(Name = "رقم الجوال")]
        [Required(ErrorMessage = "رقم الجوال مطلوب")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = " البريد الالكترونى مطلوب")]
        [Display(Name = "البريد الالكترونى")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        //[RegularExpression(@"@(moh.gov.sa)\.com$", ErrorMessage = "Invalid domain in email address. The domain must be moh.gov.sa")]
        [RegularExpression(@".*@moh.gov.sa$", ErrorMessage = "Must be a @moh.gov.sa email address")]
        public string Email { get; set; }
        [Display(Name = "الرقم السرى")]
        [Required(ErrorMessage = "الرقم السرى مطلوب")]
        //[StringLength(255, ErrorMessage = "الرقم السرى يجب أن يكون بين 5 و 255 حرفًا", MinimumLength = 5)]
        [DataType(DataType.Password)]
        //[StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        public string Password { get; set; }
        [Display(Name = "تأكيد الرقم السرى")]
        [Required(ErrorMessage = "تأكيد الرقم السرى مطلوب")]
        //[StringLength(255, ErrorMessage = "الرقم السرى يجب أن يكون بين 5 و 255 حرفًا", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="لابد ان يكون الرقم السرى وتاكيد الرقم السرى متساوين")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        public string SearchKey { get; set; }
        [Display(Name = "المرفق")]
        //[Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار المرفق ")]
        public int AttachmentId { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<EmployeeAttachment> EmployeeAttachments { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        /*
         





















         
         
         */





        //public int EmployeeId { get; set; }
        //[Required(ErrorMessage = "برجاء ادخال الاسم")]
        //[Display(Name = "الاسم")]
        ////[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).*$")]
        //[MaxLength(25)]
        //public string EmployeeName { get; set; }
        //[Display(Name = "العنوان")]
        //public string Address { get; set; }
        //public string EmployeeDescription { get; set; }
        //[Required(ErrorMessage = "برجاء ادخال التصنيف")]
        //[Display(Name = "تصنيف السند")]
        //public int CategoryId { get; set; }
        //public List<Category> Categories { get; set; }
        //[Required(ErrorMessage = "برجاء ادخال تصنيف الموظفين")]
        //[Display(Name = "تصنيف الموظفين")]
        //public int EmployeeCategoryId { get; set; }
        //public List<EmployeeCategory> EmployeeCategories { get; set; }
        //public IFormFile Url { get; set; }
        //public string ImageUrl { get; set; }
        //[Required(ErrorMessage = "برجاء ادخال الرقم القومى")]
        //[Display(Name = "الرقم القومى")]
        ////[RegularExpression(@"/^(009665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$/",ErrorMessage = "لابد من ادخال الرقم القومى بالصيغة الصحيحة")]
        //public string NationalId { get; set; }
        //[Display(Name = "تاريخ الميلاد")]
        //public DateTime? BitrhDate { get; set; }

        //[Display(Name = "تاريخ الارشيف")]
        //[Required(ErrorMessage = "برجاء ادخال تاريخ الارشيف")]
        //public DateTime ArchieveDate { get; set; } = DateTime.Now;
        //[Display(Name = "رقم الجوال")]
        //public string PhoneNumber { get; set; }
    }
}
