using MedicalTest2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public class CategorytRepository// : IStoreRepo<Category>
    {
        private readonly ApplicationDbContext dbContext;

        public CategorytRepository(ApplicationDbContext bookStoreDbContext)
        {
            this.dbContext = bookStoreDbContext;
        }
        //private readonly List<Author> categories = new List<Author>()
        //    {
        //        new Author(){Id=20,Name="asda" },
        //        new Author(){Id=30,Name="qqq" },
        //        new Author(){Id=40,Name="qqq" },
        //    };
        //public AuthotRepository()
        //{
          
        //}
        //public void Add(Category entity)
        //{
        //    dbContext.Categories.Add(entity);
        //    dbContext.SaveChanges();
        //}

        //public void Delete(Category entity)
        //{
        //    var author = GetById(entity.Id);
        //    dbContext.Categories.Remove(author);
        //    dbContext.SaveChanges();
        //}

        //public List<Category> Get()
        //{
        //    return dbContext.Categories.ToList();
        //}

        //public Category GetById(int id)
        //{
        //    var author = dbContext.Categories.Find(id);
        //    return author;
        //}

        //public void Update(int id, Category entity)
        //{
        //    var author = GetById(entity.Id);
        //    author.Name = entity.Name;
        //   // bookStoreDbContext.Categories.Update(entity);
        //    dbContext.SaveChanges();

        //}
        //public bool SaveChanges()
        //{
        //  return  dbContext.SaveChanges()>0?true:false;
        //}

        //public IQueryable<Category> GetQueryable()
        //{
        //    return dbContext.Categories;
        //}
    }
}
