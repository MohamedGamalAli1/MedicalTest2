using MedicalTest2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly ISMSService _smsService;

        public SMSController(ISMSService smsService)
        {
            _smsService = smsService;
        }

        public class SendSMSDto
        {
            public string MobileNumber { get; set; }
            public string Body { get; set; }
        }
        [HttpPost("send")]
        public IActionResult Send(SendSMSDto dto)
        {
            var result = _smsService.Send(dto.MobileNumber, dto.Body);

            if (!string.IsNullOrEmpty(result.Result.ErrorMessage))
                return BadRequest(result.Result.ErrorMessage);

            return Ok(result);
        }
    }
}
