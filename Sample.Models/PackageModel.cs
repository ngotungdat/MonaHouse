using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sample.Utilities.CoreContants;

namespace Sample.Models
{
    public class PackageModel : AppDomainModel
    {
        /// <summary>
        /// Tên gói phần mềm
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Mô tả cho title
        /// </summary>
        public string TitleDescription { get; set; }
        /// <summary>
        /// Số phòng tối đa
        /// </summary>
        public int LimitedRoom { get; set; }
        /// <summary>
        /// Giá gói phần mềm
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Mô tả thêm cho gói
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Loại hình cho thuê: phòng trọ/ktx...
        /// </summary>
        public int TypeOfRental { get; set; }
        public string TypeOfRentalName
        {
            get
            {
                switch (TypeOfRental)
                {
                    case (int)EnumTypeOfRental.PhongTro:
                        return "Phòng trọ";
                    case (int)EnumTypeOfRental.KTX:
                        return "Ký túc xá";
                    default:
                        return string.Empty;
                }
            }
        }
        /// <summary>
        /// Loại gói phần mềm: Dùng thử, tính phí, lập trình riêng
        /// </summary>
        public int PackageType { get; set; }
        public string PackageTypeName
        {
            get
            {
                switch (PackageType)
                {
                    case (int)EnumPackageType.DungThu:
                        return "Dùng thử";
                    case (int)EnumPackageType.TinhPhi:
                        return "Tính phí";
                    case (int)EnumPackageType.LapTrinhRieng:
                        return "Lập trình riêng";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
