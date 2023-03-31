using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class ActualWorkRepository : IStoreRepo<ActualWork>
    {
        private readonly ApplicationDbContext dbContext;

        public ActualWorkRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ActualWork entity)
        {
            dbContext.ActualWorks.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(ActualWork entity)
        {
            var result = GetById(entity.Id);
            dbContext.ActualWorks.Remove(result);
            dbContext.SaveChanges();
        }

        public List<ActualWork> Get()
        {
            return dbContext.ActualWorks.ToList();
        }

        public ActualWork GetById(int id)
        {
            var result = dbContext.ActualWorks.Find(id);
            return result;
        }

        public void Update(int id, ActualWork entity)
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

        public IQueryable<ActualWork> GetQueryable()
        {
            return dbContext.ActualWorks;
        }

        public ActualWork GetByName(string name)
        {
            var result = dbContext.ActualWorks.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}
