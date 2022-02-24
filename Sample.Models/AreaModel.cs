//using Sample.Models.Auth;
using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Sample.Utilities.CoreContants;

namespace Sample.Models
{
    public class AreaModel : AppDomainModel
    {
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        public string Name { get; set; }
    }
}
