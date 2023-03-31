using System.ComponentModel.DataAnnotations;

namespace MedicalTest2.Models
{
    public class ActualWork
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الاسم")]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
    }
}