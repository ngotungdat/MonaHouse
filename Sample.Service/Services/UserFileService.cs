using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Service.Services.DomainServices;
using Sample.Interface.Services;

namespace Sample.Service.Services
{
    public class UserFileService : DomainService<UserFiles, BaseSearch>, IUserFileService
    {
        public UserFileService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
