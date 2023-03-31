using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class DestinationRepository : IStoreRepo<Destination>
    {
        private readonly ApplicationDbContext dbContext;

        public DestinationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Destination entity)
        {
            dbContext.Destinations.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Destination entity)
        {
            var result = GetById(entity.Id);
            dbContext.Destinations.Remove(result);
            dbContext.SaveChanges();
        }

        public List<Destination> Get()
        {
            return dbContext.Destinations.ToList();
        }

        public Destination GetById(int id)
        {
            var result = dbContext.Destinations.Find(id);
            return result;
        }

        public void Update(int id, Destination entity)
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

        public IQueryable<Destination> GetQueryable()
        {
            return dbContext.Destinations;
        }
        public Destination GetByName(string name)
        {
            var result = dbContext.Destinations.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}
