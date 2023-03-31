using MedicalTest2.Data;
using MedicalTest2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class EmployeeRepository : IStoreRepo<Employee>
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Employee entity)
        {
            dbContext.Employees.Add(entity);
            dbContext.SaveChanges();
        }

        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public void Delete(Employee entity)
        {
            var employee = GetById(entity.Id);
            //dbContext.Employees.Remove(book);
            employee.IsRetired = true;
            dbContext.SaveChanges();
        }
        /*
      nationalityRepo
      destinationRepo
     departmentRepo
      actualWorkRepo
      jobCategoryRepo


    SelectDdlNationality
    SelectDdlDestination
    SelectDdlDepartment
    SelectDdlActualWork
    SelectDdlJobCategory


    Nationality
    Destination
    Department
    ActualWork
    JobCategory
     */
        public List<Employee> Get()
        {
            var result = dbContext.Employees.AsNoTracking().Include(e => e.Nationality).OrderBy(r => r.Id).ToList();
           // var result = dbContext.Employees.AsNoTracking().Include(e => e.Nationality).OrderBy(r => r.Id).ToList();
            return result;
        }
        public IQueryable<Employee> GetQueryable()
        {
            var result = dbContext.Employees.Include(e => e.Nationality).Include(e => e.Destination).Include(e => e.Department).Include(e => e.ActualWork).Include(e => e.JobCategory).Include(e => e.WorkType).OrderBy(r => r.Id);
            //result.Select(r => new EmployeeDataVM
            //{
            //    Id = r.Id,
            //    CreationDate = r.CreationDate,
            //    Name = r.FirstName + " " + r.SecondName + " " + (!string.IsNullOrWhiteSpace(r.FamilyName) ? r.FamilyName : ""),
            //    NationalId = r.NationalId,
            //    DestinationName = r.Destination.Name,
            //    DepartmentName = r.Department.Name,
            //    ComputerNumber = r.ComputerNumber,
            //    JobCategoryName = r.JobCategory.Name,
            //    ActualWorkName = r.ActualWork.Name,
            //    WorkTypeName = r.WorkType.Name,
            //    PhoneNumber = r.PhoneNumber,
            //    Email = dbContext.Users.FirstOrDefault(x => x.EmployeeId == r.Id).Email,
            //    IsRetired = r.IsRetired,
            //}).ToList();
            return result;
        }

        public Employee GetById(int id)
        {
            return dbContext.Employees.Include(e => e.Nationality).Include(e => e.Destination).Include(e => e.Department).Include(e => e.ActualWork).Include(e => e.JobCategory).Include(e => e.WorkType).FirstOrDefault(r => r.Id == id);
        }

        public void Update(int id, Employee entity)
        {

            var employee = GetById(id);
            //employee.Description = entity.Description;
            //employee.Name = entity.Name;
            //employee.ImageUrl = entity.ImageUrl;
            //employee.CategoryId = entity.CategoryId;
            //employee.NationalityId = entity.NationalityId;
            //employee.Address = entity.Address;
            //employee.ArchieveDate = entity.ArchieveDate;
            //employee.BitrhDate = entity.BitrhDate;
            //employee.NationalId = entity.NationalId;
            //employee.PhoneNumber = entity.PhoneNumber;


            //employee.FirstName = entity.FirstName;
            //employee.SecondName = entity.SecondName;
            //employee.FamilyName = entity.FamilyName;
            ////employee.Nationality = nationalityRepo.GetById(model.NationalityId);
            ////employee.Destination = destinationRepo.GetById(model.DestinationId);
            ////employee.Department = departmentRepo.GetById(model.DepartmentId);
            ////employee.ActualWork = actualWorkRepo.GetById(model.ActualWorkId);
            ////employee.JobCategory = jobCategoryRepo.GetById(model.JobCategoryId);
            ////employee.EmployeeCategory = empoyeeCatRepo.GetById(model.EmployeeCategoryId),
            //employee.NationalityId = entity.NationalityId;
            //employee.DestinationId = entity.DestinationId;
            //employee.DepartmentId = entity.DepartmentId;
            //employee.ActualWorkId = entity.ActualWorkId;
            //employee.JobCategoryId = entity.JobCategoryId;
            //employee.NationalId = entity.NationalId;
            //employee.Gender = entity.Gender;
            ////employee.IslamicDate = entity.IslamicDate;
            //employee.BitrhDate = entity.BitrhDate;
            ////employee.CreationDate = DateTime.UtcNow;
            //employee.ComputerNumber = entity.ComputerNumber;
            //employee.WorkType = entity.WorkType;
            //employee.PhoneNumber = entity.PhoneNumber;
            //employee.Email = entity.Email;
            //employee.Password = entity.Password;
            //employee.ConfirmPassword = entity.ConfirmPassword;
            dbContext.Employees.Update(employee);

            dbContext.SaveChanges();
        }
        public Employee GetByName(string name)
        {
            var result = dbContext.Employees.FirstOrDefault(r => r.FirstName == name);
            return result;
        }
    }
}
