using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest
{
    public class RequestType
    {
        /// <summary>
        /// Nhập lệnh báo giá
        /// </summary>
        public const int NewConditionOrder = 1;

        /// <summary>
        /// Sửa lệnh báo giá trong trường hợp sửa tại công ty
        /// </summary>
        public const int EditOrder = 2;

        /// <summary>
        /// Sửa lệnh báo giá
        /// </summary>
        public const int EditConditionOrder = 3;

        /// <summary>
        /// Sửa giá sàn HA
        /// </summary>
        public const int EditPrice = 4;

        /// <summary>
        /// Hủy lệnh trong trường hợp hủy lệnh tại công ty
        /// </summary>
        public const int CancelOrder = 5;

        /// <summary>
        /// Hủy lệnh báo giá
        /// </summary>
        public const int CancelConditionOrder = 6;

        /// <summary>
        /// Nhập/Hủy lệnh quảng cáo
        /// </summary>
        public const int NewAdvertisement = 7;

        /// <summary>
        /// Nhập lệnh one firm
        /// </summary>
        public const int NewOneFirm = 8;

        /// <summary>
        /// Hủy lệnh one firm
        /// </summary>
        public const int CancelOneFirm = 9;

        /// <summary>
        /// Nhập lệnh two firm
        /// </summary>
        public const int NewTwoFirm = 10;

        /// <summary>
        /// Chấp nhận lệnh two firm
        /// </summary>
        public const int ApproveTwoFirm = 11;

        /// <summary>
        /// Yêu cầu hủy lệnh two firm
        /// </summary>
        public const int CancelTwoFirm = 12;

        /// <summary>
        /// Chấp nhận hủy lệnh two firm
        /// </summary>
        public const int ApproveCancelTwoFirm = 13;

        /// <summary>
        /// Thêm mới lệnh thỏa thuận điện tử UpCom 
        /// </summary>
        public const int UpComNewOrder = 14;
        /// <summary>
        /// Sửa lệnh thỏa thuận điện tử UpCom
        /// </summary>	
        public const int UpComEditOrder = 15;
        /// <summary>
        /// Hủy lệnh thỏa thuận điện tử UpCom
        /// </summary>
        public const int UpcomCancelOrder = 16;
        /// <summary>
        /// Thêm lệnh 1Firm UpCom
        /// </summary>
        public const int UpComNewOneFirm = 17;
        /// <summary>
        /// Sửa lệnh 1Firm UpCom
        /// </summary>
        public const int UpComEditOneFirm = 18;
        /// <summary>
        /// Hủy lệnh 1Firm UpCom
        /// </summary>
        public const int UpcomCancelOneFirm = 19;
        /// <summary>
        /// Thêm lệnh 2Firm UpCom
        /// </summary>
        public const int UpComNewTwoFirm = 20;
        /// <summary>
        /// Sửa lệnh 2Firm UpCom
        /// </summary>
        public const int UpComEditTwoFirm = 21;
        /// <summary>
        /// Hủy lệnh 2Firm UpCom
        /// </summary>
        public const int UpcomCancelTwoFirm = 22;
    }

}
