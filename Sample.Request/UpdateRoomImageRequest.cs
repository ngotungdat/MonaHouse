using Sample.Entities;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class UpdateRoomImageRequest 
    {
        public int? RoomId { get; set; }
        /// <summary>
        /// link lưu hình ảnh
        /// </summary>
        public string StringAddImageLinks { get; set; }
        public string StringdeleteImageId { get; set; }
    }
}
