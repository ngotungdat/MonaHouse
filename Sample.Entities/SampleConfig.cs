using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class SampleConfig : DomainEntities.AppDomain
    {
        /// <summary>
        /// Loại 
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
    }
}
