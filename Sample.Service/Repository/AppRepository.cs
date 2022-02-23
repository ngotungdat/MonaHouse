using Sample.Entities.DomainEntities;
using Sample.Interface.DbContext;
using Sample.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Service.Repository
{
    public class AppRepository<T> : DomainRepository<T>, IAppRepository<T> where T : Entities.DomainEntities.AppDomain
    {
        public AppRepository(IAppDbContext context) : base(context)
        {

        }

    }
}
