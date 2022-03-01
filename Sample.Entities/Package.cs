﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class Package : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên gói phần mềm
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Mô tả cho title
        /// </summary>
        [Required]
        public string TitleDescription { get; set; }
        /// <summary>
        /// Số phòng tối đa
        /// </summary>
        [Required]
        public int LimitedRoom { get; set; }
        /// <summary>
        /// Giá gói phần mềm
        /// </summary>
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// Mô tả thêm cho gói
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}