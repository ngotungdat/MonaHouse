using Sample.Entities.DomainEntities;
using Sample.Interface.DbContext;
using Sample.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Service.Repository
{
    public class CatalogueRepository<T> : DomainRepository<T>, ICatalogueRepository<T> where T: AppDomainCatalogue
    {
        public CatalogueRepository(IAppDbContext context) : base(context)
        {

        }

        public T GetByCode(string code)
        {
            return Context.Set<T>().FirstOrDefault(e => e.Code == code);
        }

        public void Delete(int id)
        {
            T entity = Context.Set<T>().FirstOrDefault(e => e.Id == id);
            entity.Deleted = true;
            Context.Set<T>().Update(entity);
        }
        public override void Delete(T entity)
        {
            entity.Deleted = true;
            Context.Set<T>().Update(entity);
        }
    }
}
