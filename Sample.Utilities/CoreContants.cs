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

        /// <summary>
        /// Trạng thái của người dùng
        /// </summary>
        public enum StatusUser
        {
            Active = 0,
            NotActive = 1,
            Locked = 2
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
