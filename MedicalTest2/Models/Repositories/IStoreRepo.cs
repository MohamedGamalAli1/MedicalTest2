using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalTest2.Models.Repositories
{
    public interface IStoreRepo<Entity>
    {
        List<Entity> Get();
        Entity GetById(int id);
        void Add(Entity entity);
        void Update(int id,Entity entity);
        void Delete(Entity entity);
        bool SaveChanges();
    }
}
