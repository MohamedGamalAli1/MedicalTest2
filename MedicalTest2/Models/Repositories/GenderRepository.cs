using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class GenderRepository : IStoreRepo<Gender>
    {
        private readonly ApplicationDbContext dbContext;

        public GenderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Gender entity)
        {
            dbContext.Genders.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Gender entity)
        {
            var result = GetById(entity.Id);
            dbContext.Genders.Remove(result);
            dbContext.SaveChanges();
        }

        public List<Gender> Get()
        {
            var res = dbContext.Genders.ToList();
            return res;
        }

        public Gender GetById(int id)
        {
            var result = dbContext.Genders.Find(id);
            return result;
        }

        public void Update(int id, Gender entity)
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

        public IQueryable<Gender> GetQueryable()
        {
            return dbContext.Genders;
        }
        public Gender GetByName(string name)
        {
            var result = dbContext.Genders.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}