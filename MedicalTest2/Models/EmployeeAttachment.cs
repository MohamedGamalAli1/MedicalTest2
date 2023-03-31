using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MedicalTest2.Models
{
    public class EmployeeAttachment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اختر اسم المرفق")]
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool IsSent { get; set; }
        public DateTime SentDate { get; set; }

    }
}
