using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Vehicle : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên xe
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Loại xe
        /// </summary>
        public int? VehicleFeeConfigId { get; set; }
        /// <summary>
        /// Biển số xe
        /// </summary>
        public string LicensePlates { get; set; }
        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Khách hàng
        /// </summary>
        public int? CustomerId { get; set; }
    }
}
