using MedicalTest2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalTest2.ViewModels
{
    public class AddEmployeeAttachmentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "اختر اسم المرفق")]
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "المرفق")]
        //[Range(1, int.MaxValue, ErrorMessage = "برجاء اختيار المرفق ")]
        public int AttachmentId { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
