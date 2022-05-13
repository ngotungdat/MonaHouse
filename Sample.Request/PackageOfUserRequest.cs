using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class PackageOfUserRequest: AppDomainRequest
    {
        /// <summary>
        /// ID user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ID gói phần mềm
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// Trạng thái gói đăng kí của người dùng
        /// </summary>
        public int Status { get; set; }

        // not mapping
        public string Avatar { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public double? DebtMoney { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CitizenIdentification { get; set; }

        public int? GenderNumber { get; set; }

        public string TitleDescription { get; set; }

        public int UserdTime { get; set; }

        public double? Price { get; set; }

        public string Description { get; set; }

        public int? LimitedRoom { get; set; }

        public int PackageType { get; set; }

        public string Title { get; set; }

        public int TypeOfRental { get; set; }
    }
}
