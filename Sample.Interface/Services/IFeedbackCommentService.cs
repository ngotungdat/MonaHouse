using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IFeedbackCommentService : IDomainService<FeedbackComment,BaseSearch>
    {
        public Task<PagedList<FeedbackComment>> GetFeedbackCommentByFeedbackId(BaseSearch baseSearch);
    }
}
