using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Utilities
{
    public class CoreContants
    {
        public const string ViewAll = "ViewAll";
        public const string View = "View";
        public const string AddNew = "AddNew";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string Import = "Import";
        public const string Upload = "Upload";
        public const string Download = "Download";
        public const string Export = "Export";

        //public const string FullControl = "FullControl";
        //public const string Approve = "Approve";
        //public const string DeleteFile = "DeleteFile";

        public const string UPLOAD_FOLDER_NAME = "Upload";
        public const string TEMP_FOLDER_NAME = "Temp";
        public const string TEMPLATE_FOLDER_NAME = "Template";
        public const string CATALOGUE_TEMPLATE_NAME = "CatalogueTemplate.xlsx";
        public const string USER_FOLDER_NAME = "User";
        public const string QR_CODE_FOLDER_NAME = "QRCode";

        public const string GET_TOTAL_NOTIFICATION = "get-total-notification";

        public enum RoleUser
        { 
            Admin = 1,
            CSKH = 2,
            ChuTro = 3,
            NhanVien = 4,
            ThueTro = 5,
        }
        /// <summary>
        /// Trạng thái của người dùng
        /// </summary>
        public enum StatusUser
        {
            Active = 0,
            NotActive = 1,
            Locked = 2
        }
        public enum Gender
        {
            Nam = 1,
            Nu = 2,
            KhongXacDinh = 3
        }
        /// <summary>
        /// Danh mục quyền
        /// </summary>
        public enum PermissionContants
        {
            ViewAll = 1,
            View = 2,
            AddNew = 3,
            Update = 4,
            Delete = 5,
            Import = 6,
            Upload = 7,
            Download = 8,
            Export = 9
        }
        public enum EnumBranchType
        {
            Cuahang = 1,
            NhaTro = 2,
            NhaNguyenCan = 3,
            KyTucXa = 4,
        }
        public enum EnumRentalForm
        {
            BaoPhong = 1,
            KyTucXa = 2,
        }
        public enum EnumRoomStatus
        {
            PhongTrong = 1,
            DaCoc = 2,
            DangO = 3,
        }
        public enum EnumCustomerStatus
        {
            DangO = 1,
            DaChuyenDi = 2,
        }
        public enum EnumContractStatus
        {
            ConHan = 1,
            HetHan = 2,
        }
        /// <summary>
        /// Loại hình cho thuê gói phần mềm
        /// </summary>
        public enum EnumTypeOfRental
        {
            PhongTro = 1,
            KTX = 2,
        }
        /// <summary>
        /// Loại gói phần mềm
        /// </summary>
        public enum EnumPackageType
        {
            DungThu = 1,
            TinhPhi = 2,
            LapTrinhRieng = 3
        }
        //#region SMS Template
        ///// <summary>
        ///// Xác nhận OTP SMS
        ///// </summary>
        public const string SMS_XNOTP = "XNOTP";
        //#endregion

        //#region Email Template
        //#endregion
    }
}
