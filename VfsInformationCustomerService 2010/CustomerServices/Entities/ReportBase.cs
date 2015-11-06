using System;
using System.Collections.Generic;
using System.Text;

namespace VfsCustomerService.Entities
{
    [Serializable]
    public class ReportBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string UploadDir { get; set; }
        public Nullable<System.DateTime> DateViewCustomer { get; set; }
        public Nullable<long> TotalDownload { get; set; }
        public Nullable<int> FileSize { get; set; }
        public Nullable<int> IdReportType { get; set; }
        public string Ticker { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string FileType { get; set; }
    }

    public enum ReportColumns
    {
        Id,
        Title,
        UploadDir,
        DateViewCustomer,
        TotalDownload,
        FileSize,
        IdReportType,
        Ticker,
        CreateDate,
        FileType
    }
}
