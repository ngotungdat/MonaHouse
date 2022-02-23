using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Models.Configuration
{
    public class SMSConfiguartionModel : AppDomainModel
    {
        public string APIKey { get; set; }

        public string SecretKey { get; set; }

        public string BrandName { get; set; }

        public int SMSType { get; set; }

        /// <summary>
        /// Cú pháp tin nhắn mẫu
        /// </summary>
        public string TemplateText { get; set; }

        /// <summary>
        /// Url web service
        /// </summary>
        public string WebServiceUrl { get; set; }
    }
}
