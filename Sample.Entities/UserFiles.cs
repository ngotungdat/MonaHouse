using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Entities
{
    public class UserFiles : AppDomainFile
    {
        /// <summary>
        /// Loại file
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// Id của user
        /// </summary>
        public int? UserId { get; set; }
    }
}
