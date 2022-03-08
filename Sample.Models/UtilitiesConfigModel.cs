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
    public class UtilitiesConfigModel : AppDomainModel
    {
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
