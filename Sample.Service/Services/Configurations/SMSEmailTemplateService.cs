using Sample.Entities;
using Sample.Entities.Configuration;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.Configuration;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services.Configurations
{
    public class SMSEmailTemplateService : CatalogueService<SMSEmailTemplates, BaseSearch>, ISMSEmailTemplateService
    {
        public SMSEmailTemplateService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

    }
}
