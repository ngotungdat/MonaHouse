using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class ReportPackageOfUser : DomainEntities.AppDomain
    {
        [NotMapped]
        public double? doanhthu { get; set; }
    }
}
