using MedicalTest2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        public UsersController(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
           var result= await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok();
            else
                throw new Exception("Exception on Deletion") ;
        }
    }
}
