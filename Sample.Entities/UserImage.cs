using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class UserImage : DomainEntities.AppDomain
    {
        [Required]
        public int? UserId { get; set; }
        /// <summary>
        /// link lưu hình ảnh
        /// </summary>
        public string Link { get; set; }
    }
}
