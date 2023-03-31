using MedicalTest2.Models;
using MedicalTest2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISMSService _smsService;

        public HomeController(ILogger<HomeController> logger, ISMSService smsService)
        {
            _logger = logger;
            _smsService = smsService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Send(SendSMSDto dto)
        {
            var result = _smsService.Send("", dto.Body);

            if (!string.IsNullOrEmpty(result.Result.ErrorMessage))
                return BadRequest(result.Result.ErrorMessage);

            return Ok(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
