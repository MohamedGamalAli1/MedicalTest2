using AspNetCore.Reporting;
using MedicalTest2.Data;
using MedicalTest2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalTest2.Services
{

    public class ReportService : IReportService
    {

        public readonly ApplicationDbContext _DB;
        private readonly IWebHostEnvironment _webHostEnvirnoment;


        public ReportService(ApplicationDbContext db, IWebHostEnvironment _webHostEnvirnoment)
        {
            _DB = db;
            this._webHostEnvirnoment = _webHostEnvirnoment;
        }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public byte[] GenerateReportAsync(string reportName, string? id)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (id == null) { id = ""; };

          //  string url = env.ContentRootPath;// Path.Combine(env.ContentRootPath, "ReportFiles");

            //string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("ReportAPI.dll", string.Empty);
           // string rdlcFilePath = string.Format("{0}\\ReportFiles\\{1}.rdlc", url, reportName);
            // string rdlcFilePath = @"C:\Users\moham\source\repos\MedicalTest2\ReportFiles\CusDetails.rdlc";
            //  string rdlcFilePath = @"C:\Users\moham\source\repos\MedicalTest2\MedicalTest2\ReportFiles\CusDetails.rdlc";
            var path = $"{_webHostEnvirnoment.WebRootPath}\\Reports\\CusDetails.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");


            LocalReport report = new LocalReport(path);
            //List<UserDto> userList = new List<UserDto>();
            //userList.Add(new UserDto
            //{
            //    FirstName = "Alex",
            //    LastName = "Smith",
            //    Email = "alex.smith@gmail.com",
            //    Phone = "2345334432"
            //});

            //userList.Add(new UserDto
            //{
            //    FirstName = "John",
            //    LastName = "Legend",
            //    Email = "john.legend@gmail.com",
            //    Phone = "5633435334"
            //});

            //userList.Add(new UserDto
            //{
            //    FirstName = "Stuart",
            //    LastName = "Jones",
            //    Email = "stuart.jones@gmail.com",
            //    Phone = "3575328535"
            //});
            IList<Employee> cuslst = new List<Employee>();
            //List<CusDto> cuslst1 = new List<CusDto>();
            //var str1 = id.ToString();//"ANTON";
            //if (id == null || id == "" || id == "null")
            //{ cuslst = _DB.Customers.ToList(); }

            //else
            //{ cuslst = _DB.Customers.Where(x => x.CustomerId.Equals(str1)).ToList(); }
            //// cuslst1 = _DB.Customers.ToList();
            //cuslst1 = (from e in cuslst


            //           select new CusDto
            //           {
            //               CustomerId = e.CustomerId,
            //               CompanyName = e.CompanyName,
            //               ContactName = e.ContactName,
            //               ContactTitle = e.ContactTitle,
            //               Address = e.Address,
            //               City = e.City,
            //               Region = e.Region,
            //               PostalCode = e.PostalCode,
            //               Country = e.Country,
            //               Phone = e.Phone,


            //           }).ToList();

            var employees = _DB.Employees.Select(r => new
            {
                r.Id,
                r.FirstName,
                r.SecondName,
                r.FamilyName

            }).ToList();

            // report.AddDataSource("dsUsers", userList);
            report.AddDataSource("DataSet1", employees);

            // report.ConsumeContainerWhiteSpace = True
           // var result = report.Execute(GetRenderType("pdf"), 1, parameters);
            var result = report.Execute(GetRenderType("pdf"));
            // var result1 = report.Execute(GetRenderType("pdf"), 1, null);
            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    renderType = RenderType.Pdf;
                    break;
                case "word":
                    renderType = RenderType.Word;
                    break;
                case "excel":
                    renderType = RenderType.Excel;
                    break;
            }

            return renderType;
        }
    }
}
