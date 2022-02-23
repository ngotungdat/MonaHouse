using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities.DomainEntities
{
    public class AppDomain
    {
        public AppDomain()
        {
            //Created = DateTime.Now;
        }

        /// <summary>
        /// STT
        /// </summary>
        [NotMapped]
        public long RowNumber { get; set; }

        /// <summary>
        /// Tổng số item phân trang
        /// </summary>
        [NotMapped]
        public int TotalItem { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [NoMapCreated]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Tạo bởi
        /// </summary>
        [StringLength(100)]
        [NoMapCreated]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        [NoMapUpdated]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        [StringLength(100)]
        [NoMapUpdated]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Cờ xóa
        /// </summary>
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Cờ active
        /// </summary>
        [DefaultValue(true)]
        public bool Active { get; set; }

    }
}
