using MedicalTest2.Models;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.ViewModels
{
    public class ArchieveViewModel
    {
        [Key]
        [Display(Name = "الرقم")]
        public int Id { get; set; }
        [MaxLength(20)]
        public string NationalId { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; }
        public int ActualWorkId { get; set; }

        public List<ActualWork> ActualWorks { get; set; }
        public int JobCategoryId { get; set; }
        public bool IsRetired { get; set; }

        public List<JobCategory> JobCategories { get; set; }
        public int ComputerNumber { get; set; }
        //public int CatId { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? From { get; set; } 
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? To { get; set; }
        //public List<Category> Categories { get; set; }
        //public int EmployeeCatId { get; set; }
        //public List<EmployeeCategory> EmployeeCategories { get; set; }
        //public List<Employee> Employees { get; set; }
        //public List<Nationality> Nationalities { get; set; }
        //public int Destinations { get; set; }
        //public List<Destination> Destinations { get; set; }
        //public PagingList<Employee> Employees { get; set; }
        public PagingList<EmployeeDataVM> EmployeesData { get; set; }


        //[Display(Name = "الاسم")]
        //public string Name { get; set; }

        //[Display(Name = "تاريخ التسجيل")]
        //public string CreationDate { get; set; }
        //[Display(Name = "الحالة")]
        //public bool IsRetired { get; set; }
        //[Display(Name = "رقم الحاسب الآلى")]
        //public int ComputerNumber { get; set; }
        //[Display(Name = "الجهة")]
        //public string DestinationName { get; set; }
        //[Display(Name = "القسم")]
        //public string DepartmentName { get; set; }
        //[Display(Name = "العمل الفعلى")]
        //public string ActualWorkName { get; set; }
        //[Display(Name = "فئة الوظيفة")]
        //public string JobCategoryName { get; set; }
        //[Display(Name = "نوع التعيبن")]
        //public string WorkTypeName { get; set; }
        //[Display(Name = "رقم الجوال")]
        //public string PhoneNumber { get; set; }
        //[Display(Name = "البريد الالكترونى")]
        //public string Email { get; set; }
   
    }
}
