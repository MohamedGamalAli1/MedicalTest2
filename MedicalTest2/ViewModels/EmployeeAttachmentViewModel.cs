using MedicalTest2.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace MedicalTest2.ViewModels
{
    public class EmployeeAttachmentViewModel
    {
        public List<EmployeeAttachment> EmployeeAttachment { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
