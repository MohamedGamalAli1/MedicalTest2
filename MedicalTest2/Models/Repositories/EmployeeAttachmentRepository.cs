using MedicalTest2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class EmployeeAttachmentRepository : IStoreRepo<EmployeeAttachment>
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeAttachmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(EmployeeAttachment entity)
        {
            dbContext.EmployeeAttachments.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(EmployeeAttachment entity)
        {
            var result = GetById(entity.Id);
            dbContext.EmployeeAttachments.Remove(result);
            dbContext.SaveChanges();
        }

        public List<EmployeeAttachment> Get()
        {
            var res = dbContext.EmployeeAttachments.Include(r=>r.Attachment).ToList();
            return res;
        }

        public EmployeeAttachment GetById(int id)
        {
            var result = dbContext.EmployeeAttachments.Include(r => r.Attachment).FirstOrDefault(r=>r.Id==id);
            return result;
        }
        public EmployeeAttachment CheckEmployeeAttachmentIfExists(int AttachmentId, int employeeId)
        {
            var result = Get().FirstOrDefault(r=>r.AttachmentId== AttachmentId && r.EmployeeId== employeeId);
            return result;
        }

        public void Update(int id, EmployeeAttachment entity)
        {
            var result = GetById(entity.Id);
            dbContext.EmployeeAttachments.Update(result);
            dbContext.SaveChanges();

        }
        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0 ? true : false;
        }

        public IQueryable<EmployeeAttachment> GetQueryable()
        {
            return dbContext.EmployeeAttachments;
        }
        public EmployeeAttachment GetByName(string employeeId)
        {
            var result = dbContext.EmployeeAttachments.FirstOrDefault(r => r.EmployeeId == int.Parse(employeeId));
            return result;
        }
    }
}