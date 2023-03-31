using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalTest2.Models
{
    public class Attachment
    {
        [Key]
        [Display(Name = "الرقم")]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "السماح بالاشعارات")]
        public bool AllowNotification { get; set; }
    }
}
