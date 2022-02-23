using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models
{
    public class UserFileModel : AppDomainFileModel
    {
        /// <summary>
        /// Loại file
        /// </summary>
        public int? TypeId { get; set; }
    }
}
