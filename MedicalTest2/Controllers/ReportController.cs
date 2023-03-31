using AspNetCore.Reporting;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
using MedicalTest2.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IStoreRepo<Employee> repo;
        private IReportService _reportService;

        public ReportController(IWebHostEnvironment _webHostEnvirnoment, IStoreRepo<Employee> repo, IReportService reportService)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this._webHostEnvirnoment = _webHostEnvirnoment;
            this.repo = repo;
            _reportService = reportService;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Print()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report2.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("DateFrom", DateTime.Now.ToString());
            //get products from product table 
            var patientList = repo.Get();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", patientList);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("DownloadPdf")]
        public IActionResult DownloadPdf()
        {
            string reportName = "CusDetails";
            string? id = "";
            var returnString = _reportService.GenerateReportAsync(reportName, id);
            //    return File(returnString, "application/pdf", reportName + ".pdf");
            return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + ".pdf");
        }
        [HttpGet]
        public IActionResult Get()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{_webHostEnvirnoment.WebRootPath}\\Reports\\CusDetails.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("DateFrom", DateTime.Now.ToString());
            //get products from product table 
            var employees = repo.Get();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", employees);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");


        }
    }
}
