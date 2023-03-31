using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class WorkTypeRepository : IStoreRepo<WorkType>
    {
        private readonly ApplicationDbContext dbContext;

        public WorkTypeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(WorkType entity)
        {
            dbContext.WorkTypes.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(WorkType entity)
        {
            var result = GetById(entity.Id);
            dbContext.WorkTypes.Remove(result);
            dbContext.SaveChanges();
        }

        public List<WorkType> Get()
        {
            var res = dbContext.WorkTypes.ToList();
            return res;
        }

        public WorkType GetById(int id)
        {
            var result = dbContext.WorkTypes.Find(id);
            return result;
        }

        public void Update(int id, WorkType entity)
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

        public IQueryable<WorkType> GetQueryable()
        {
            return dbContext.WorkTypes;
        }
        public WorkType GetByName(string name)
        {
            var result = dbContext.WorkTypes.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}