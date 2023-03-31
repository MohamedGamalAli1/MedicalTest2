using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class AttachmentRepository : IStoreRepo<Attachment>
    {
        private readonly ApplicationDbContext dbContext;

        public AttachmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Attachment entity)
        {
            dbContext.Attachments.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(Attachment entity)
        {
            var result = GetById(entity.Id);
            dbContext.Attachments.Remove(result);
            dbContext.SaveChanges();
        }

        public List<Attachment> Get()
        {
            var res = dbContext.Attachments.ToList();
            return res;
        }

        public Attachment GetById(int id)
        {
            var result = dbContext.Attachments.Find(id);
            return result;
        }

        public void Update(int id, Attachment entity)
        {
            var result = GetById(entity.Id);
            result.Name = entity.Name;
            result.AllowNotification = entity.AllowNotification;
            // dbContext.Categories.Update(entity);
            dbContext.SaveChanges();

        }
        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public IQueryable<Attachment> GetQueryable()
        {
            return dbContext.Attachments;
        }
        public Attachment GetByName(string name)
        {
            var result = dbContext.Attachments.FirstOrDefault(r => r.Name == name);
            return result;
        }
    }
}