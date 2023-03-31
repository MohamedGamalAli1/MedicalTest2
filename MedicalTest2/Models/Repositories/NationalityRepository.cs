using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class NationalityRepository : IStoreRepo<Nationality>
    {
        private readonly ApplicationDbContext dbContext;

        public NationalityRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Nationality entity)
        {
            dbContext.Nationalities.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Nationality entity)
        {
            var result = GetById(entity.Id);
            dbContext.Nationalities.Remove(result);
            dbContext.SaveChanges();
        }

        public List<Nationality> Get()
        {
            return dbContext.Nationalities.ToList();
        }

        public Nationality GetById(int id)
        {
            var result = dbContext.Nationalities.Find(id);
            return result;
        }

        public void Update(int id, Nationality entity)
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

        public IQueryable<Nationality> GetQueryable()
        {
            return dbContext.Nationalities;
        }
        public Nationality GetByName(string name)
        {
            var result = dbContext.Nationalities.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}
