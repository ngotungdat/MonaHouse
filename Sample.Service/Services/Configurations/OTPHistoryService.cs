using Sample.Entities.Configuration;
using Sample.Entities.DomainEntities;
using Sample.Interface;
using Sample.Interface.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Service.Services.DomainServices;
using Sample.Interface.Services.Configuration;

namespace Sample.Service.Services.Configurations
{
    public class OTPHistoryService : DomainService<OTPHistories, BaseSearch>, IOTPHistoryService
    {
        public OTPHistoryService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
