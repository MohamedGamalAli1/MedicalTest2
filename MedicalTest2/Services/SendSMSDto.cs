using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class SendSMSDto
    {
        public int Id { get; set; }
        //public string MobileNumber { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الرسالة")]
        public string Body { get; set; }
        public EmployeeAttachment EmployeeAttachment { get; set; }
    }
}
