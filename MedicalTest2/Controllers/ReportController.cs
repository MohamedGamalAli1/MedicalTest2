using AspNetCore.Reporting;
using MedicalTest2.Models;
using MedicalTest2.Models.Repositories;
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

        public ReportController(IWebHostEnvironment _webHostEnvirnoment, IStoreRepo<Employee> repo)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this._webHostEnvirnoment = _webHostEnvirnoment;
            this.repo = repo;
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
    }
}
