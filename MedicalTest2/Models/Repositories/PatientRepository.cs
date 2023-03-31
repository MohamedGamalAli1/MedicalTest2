using MedicalTest2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class PatientRepository : IStoreRepo<Employee>
    {
        private readonly ApplicationDbContext dbContext;

        public PatientRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //private readonly List<Book> Patients;

        //public BookRepository()
        //{
        //    Patients = new List<Book>
        //   {
        //    //    new Book{ Id=1,Name="ahmed",Description=""},
        //    //    new Book{ Id=2,Name="ahmed",Description=""},
        //        new Book{ Id=2,Author=new Author{ Id=20,Name="alaa"},Name="mohamed",Description="Description 2",ImageUrl="IMG20210321090638.jpg"},
        //        new Book{ Id=3,Author=new Author{ Id=30,Name="soha"},Name="hamed",Description=" Description 33",ImageUrl="IMG20210321090638.jpg"},
        //        new Book{ Id=4,Author=new Author{ Id=40,Name="mona"},Name="salah",Description=" Description 4",ImageUrl="IMG20210321090638.jpg"},
        //    };
        //}
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
            var book = GetById(entity.Id);
            dbContext.Employees.Remove(book);
            dbContext.SaveChanges();
        }

        public List<Employee> Get()
        {
            return dbContext.Employees.Include(e => e.Category).ToList();
        }

        public Employee GetById(int id)
        {
            return dbContext.Employees.Include(e => e.Category).FirstOrDefault(r => r.Id == id);
        }

        public void Update(int id, Employee entity)
        {
            var patient = GetById(id);
            patient.Description = entity.Description;
            patient.Name = entity.Name;
            patient.ImageUrl = entity.ImageUrl;
            patient.CategoryId = entity.CategoryId;
            dbContext.SaveChanges();
        }
    }
}
