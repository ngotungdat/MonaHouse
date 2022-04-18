using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class PaymentMethod : DomainEntities.AppDomainCatalogue
    {
        /// <summary>
        /// logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// PaymentCode
        /// </summary>
        public string PaymentCode{ get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}
