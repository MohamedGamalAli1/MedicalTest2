using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class EmployeeCategoryRepository //: IStoreRepo<EmployeeCategory>
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeCategoryRepository(ApplicationDbContext bookStoreDbContext)
        {
            this.dbContext = bookStoreDbContext;
        }
        //public void Add(EmployeeCategory entity)
        //{
        //    dbContext.EmployeeCategories.Add(entity);
        //    dbContext.SaveChanges();
        //}

        //public void Delete(EmployeeCategory entity)
        //{
        //    var category = GetById(entity.Id);
        //    dbContext.EmployeeCategories.Remove(category);
        //    dbContext.SaveChanges();
        //}

        //public List<EmployeeCategory> Get()
        //{
        //    return dbContext.EmployeeCategories.ToList();
        //}

        //public EmployeeCategory GetById(int id)
        //{
        //    var category = dbContext.EmployeeCategories.Find(id);
        //    return category;
        //}

        //public void Update(int id, EmployeeCategory entity)
        //{
        //    var author = GetById(entity.Id);
        //    author.Name = entity.Name;
        //    // bookStoreDbContext.Categories.Update(entity);
        //    dbContext.SaveChanges();

        //}
        //public bool SaveChanges()
        //{
        //    return dbContext.SaveChanges() > 0 ? true : false;
        //}

        //public IQueryable<EmployeeCategory> GetQueryable()
        //{
        //    return dbContext.EmployeeCategories;
        //}
    }
}
