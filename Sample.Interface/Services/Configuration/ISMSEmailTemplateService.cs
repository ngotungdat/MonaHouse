using Sample.Entities;
using Sample.Entities.Configuration;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Interface.Services.Configuration
{
    public interface ISMSEmailTemplateService : ICatalogueService<SMSEmailTemplates, BaseSearch>
    {
    }
}
