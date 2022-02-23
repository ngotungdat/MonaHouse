using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Repository
{
    public interface IAppRepository<T> : IDomainRepository<T> where T : Entities.DomainEntities.AppDomain
    {
    }
}
