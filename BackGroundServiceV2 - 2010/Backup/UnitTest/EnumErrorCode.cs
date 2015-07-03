using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest
{
    public enum EnumErrorCode
    {
        #region định nghĩa chung
        /// <summary>
        /// Lỗi liên quan đến SQL
        /// </summary>
        sqlException,
        /// <summary>
        /// Kết quả hoạt động bình thường
        /// </summary>
        success,
        /// <summary>
        /// Lỗi không xác định
        /// </summary>
        notDefined,
        /// <summary>
        /// Người dùng không ở trạng thái hoạt động
        /// </summary>
        userNotActive,
        /// <summary>
        /// Người dùng không tồn tại
        /// </summary>
        userNotExists,
        /// <summary>
        /// Session server quá hạn
        /// </summary>
        sessionExpired,
        /// <summary>
        /// Người dùng chưa thực hiện cập nhật nhưng đã thực hiện gọi hàm
        /// </summary>
        userNotLogin,
        /// <summary>
        /// Quyền server do client gọi server không có
        /// </summary>
        serverRoleNotDefined,
        /// <summary>
        /// Người dùng không có quyền server
        /// </summary>
        notHavingServerRole,
        /// <summary>
        /// Trong trường hợp tìm một giá trị nhưng lại trả ra nhiều hơn một giá trị.
        /// </summary>
        toManyDataFound,
        /// <summary>
        /// Không có dữ liệu trả về.
        /// </summary>
        noDataFound,
        #endregion

        #region Common Validation
        /// <summary>
        /// Mã chi nhánh không tồn tại
        /// </summary>
        BranchCodeNotExist,
        /// <summary>
        /// Định dạng mã chi nhánh sai
        /// </summary>
        BranchCoceInvalid,
        /// <summary>
        /// Mã điểm giao dịch không tồn tại
        /// </summary>
        TradeCodeNotExist,
        /// <summary>
        /// Định dạng TradeCode sai
        /// </summary>
        TradeCodeInvalid,
        /// <summary>
        /// Loại sàn không tồn tại
        /// </summary>
        BoardTypeNotExist,
        /// <summary>
        /// Loại B/S không tồn tại
        /// </summary>
        OrderSideNotExist,
        /// <summary>
        /// Loại lệnh không tồn tại
        /// </summary>
        OrderTypeNotExist,
        /// <summary>
        /// Mã chứng khoán chưa được phép hoạt động
        /// </summary>
        StockCodeHalted,
        /// <summary>
        /// Mã khách hàng không tồn tại
        /// </summary>
        CustomerIdNotExist,
        /// <summary>
        /// Định dạng mã khách hàng sai
        /// </summary>
        CustomerIdInvalid,
        /// <summary>
        /// Khối lượng đặt = 0
        /// </summary>
        OrderVolumeNotFound,
        /// <summary>
        /// Lệnh phải là thỏa thuận
        /// </summary>
        OrderTypeMustPT,
        /// <summary>
        /// Khối lượng đặt không tròn lô
        /// </summary>
        OrderLotVolumeInvalid,
        /// <summary>
        /// Khối lượng đặt không đủ để giao dịch thỏa thuận
        /// </summary>
        OrderTypeCannotPT,
        /// <summary>
        /// Không đủ chứng khoán để bán
        /// </summary>
        OrderStockNotEnough,
        /// <summary>
        /// Giá đặt không hợp lệ
        /// </summary>
        OrderPriceInvalid,
        /// <summary>
        /// Giá đặt thấp hơn giá sàn
        /// </summary>
        OrderPriceInvalidFloor,
        /// <summary>
        /// Giá đặt cao hơn giá trần
        /// </summary>
        OrderPriceInvalidCeil,
        /// <summary>
        /// Không tìm được bước giá
        /// </summary>
        OrderStepPriceNotFound,
        /// <summary>
        /// Sai bước giá
        /// </summary>
        OrderStepPriceInvalid,
        /// <summary>
        /// Khách hàng không đủ tiền để đặt lệnh
        /// </summary>
        OrderMoneyNotEnough,
        #endregion

        #region RequestOrder
        /// <summary>
        /// Trạng thái yêu cầu không hợp lệ
        /// </summary>
        InvalidRequestStatus,
        #endregion

        #region Gate2Ex
        /// <summary>
        /// Gate2Ex reject
        /// </summary>
        Gate2ExReject,
        #endregion

        #region Gate2HNX
        Gate2HNXReject,
        #endregion
    }
}
