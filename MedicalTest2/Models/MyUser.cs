using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models
{
    public class MyUser:IdentityUser
    {
        [Required,MaxLength(150)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Password { get; set; }
    }
}
