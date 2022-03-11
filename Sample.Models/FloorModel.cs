using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class FloorModel: AppDomainModel
    {
        /// <summary>
        /// Tên dãy nhà
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
    }
}
