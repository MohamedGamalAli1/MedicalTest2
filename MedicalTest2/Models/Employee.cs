using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class Employee
    {
        //[Display(Name = "الرقم")]
        //public int Id { get; set; }
        //[Required(ErrorMessage = "برجاء ادخال الاسم")]
        //[Display(Name = "الاسم")]
        //public string Name { get; set; }
        //[Display(Name = "رقم الجوال")]
        //public string PhoneNumber { get; set; }
        //[Display(Name = "الرقم القومى")]
        //public string NationalId { get; set; }
        //public DateTime? BitrhDate { get; set; }
        //public string Address { get; set; }
        //[Display(Name = "الوصف")]
        //public string Description { get; set; }

        //public DateTime ArchieveDate { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
        //public int EmployeeCategoryId { get; set; }
        //public EmployeeCategory EmployeeCategory { get; set; }
        //public string ImageUrl { get; set; }
        [Key]
        [Display(Name = "الرقم")]
        public int Id { get; set; }
        [Display(Name = " الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = " اسم الاب")]
        public string SecondName { get; set; }
        [Display(Name = " اسم العائلة")]
        public string FamilyName { get; set; }
        [Display(Name = "الجنسية")]
        public int NationalityId { get; set; }
        public Nationality Nationality { get; set; }
        [Display(Name = "رقم الهوية / الإقامة")]
        public string NationalId { get; set; }
        [Display(Name = "التاريج الهجرى")]
        public DateTime? IslamicDate { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        public DateTime? BitrhDate { get; set; }
        [Display(Name ="تاريخ التسجيل")]
        public DateTime CreationDate { get; set; }
        //public string CreatedBy { get; set; }
        [Display(Name ="الحالة")]
        public bool IsRetired { get; set; }
        [Display(Name = "رقم الحاسب")]
        public int ComputerNumber { get; set; }
        [Display(Name = "الجهة")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        [Display(Name = "الجنس")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "القسم")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [Display(Name = "العمل الفعلى")]
        public int ActualWorkId { get; set; }
        public ActualWork ActualWork { get; set; }
        [Display(Name = "فئة الوظيفة")]
        public int JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }
        [Display(Name = "نوع التعيين")]
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
        //public int WorkType { get; set; }
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }
        //[Display(Name = "مرفق")]
        //public int AttachmentId { get; set; }
        //public Attachment Attachment { get; set; }
        //[Display(Name = "الرقم السرى")]
        //public string Password { get; set; }
        //[Display(Name = "تأكيد الرقم السرى")]
        //public string ConfirmPassword { get; set; }
        // public GenderId Gender { get; set; }

    }
}
