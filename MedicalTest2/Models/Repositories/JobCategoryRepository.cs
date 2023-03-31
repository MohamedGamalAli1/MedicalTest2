using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class JobCategoryRepository : IStoreRepo<JobCategory>
    {
        private readonly ApplicationDbContext dbContext;

        public JobCategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(JobCategory entity)
        {
            dbContext.JobCategories.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(JobCategory entity)
        {
            var result = GetById(entity.Id);
            dbContext.JobCategories.Remove(result);
            dbContext.SaveChanges();
        }

        public List<JobCategory> Get()
        {
            return dbContext.JobCategories.ToList();
        }

        public JobCategory GetById(int id)
        {
            var result = dbContext.JobCategories.Find(id);
            return result;
        }

        public void Update(int id, JobCategory entity)
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

        public IQueryable<JobCategory> GetQueryable()
        {
            return dbContext.JobCategories;
        }
        public JobCategory GetByName(string name)
        {
            var result = dbContext.JobCategories.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}