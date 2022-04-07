using AutoMapper;
using Microsoft.Data.SqlClient;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class FeedbackCommentService : DomainService<FeedbackComment, BaseSearch>, IFeedbackCommentService
    {
        protected IAppDbContext coreDbContext;

        public FeedbackCommentService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

        public async Task<PagedList<FeedbackComment>> GetFeedbackCommentByFeedbackId(BaseSearch baseSearch)
        {
            PagedList<FeedbackComment> pagedList = new PagedList<FeedbackComment>();
            SqlParameter[] parameters = GetSqlParameters(baseSearch);
            pagedList = await this.unitOfWork.Repository<FeedbackComment>().ExcuteQueryPagingAsync(this.GetStoreProcNameGetFeedbackCommentByFeedbackId(), parameters);
            pagedList.PageIndex = baseSearch.PageIndex;
            pagedList.PageSize = baseSearch.PageSize;
            return pagedList;
        }

        private string GetStoreProcNameGetFeedbackCommentByFeedbackId()
        {
            return "Get_FeedbackCommentByFeedbackId";
        }

        protected override string GetStoreProcName()
        {
            return "Get_FeedbackComment";
        }
    }
}
