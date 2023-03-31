using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class DepartmentRepository : IStoreRepo<Department>
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Department entity)
        {
            dbContext.Departments.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Department entity)
        {
            var result = GetById(entity.Id);
            dbContext.Departments.Remove(result);
            dbContext.SaveChanges();
        }

        public List<Department> Get()
        {
            return dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            var result = dbContext.Departments.Find(id);
            return result;
        }

        public void Update(int id, Department entity)
        {
            var result = GetById(entity.Id);
            result.Name = entity.Name;
            // dbContext.Categories.Update(entity);
            dbContext.SaveChanges();

        }
        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public IQueryable<Department> GetQueryable()
        {
            return dbContext.Departments;
        }
        public Department GetByName(string name)
        {
            var result = dbContext.Departments.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}