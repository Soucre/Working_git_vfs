using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class StockTaiSanReport
    {

        public StockTaiSanReport(string MaTk,String type)
        {
            this.Type = type;
            this.MaTk = MaTk;
            this.TaiSan.Columns.Add("ten");
            this.TaiSan.Columns.Add("dauky");
            this.TaiSan.Columns.Add("cuoiky");

            this.No.Columns.Add("ten");
            this.No.Columns.Add("dauky");
            this.No.Columns.Add("cuoiky");

            this.TaiSanKhac.Columns.Add("ten");
            this.TaiSanKhac.Columns.Add("dauky");
            this.TaiSanKhac.Columns.Add("cuoiky");

            //this.TaiSanRong.Columns.Add("ten");
            //this.TaiSanRong.Columns.Add("dauky");
            //this.TaiSanRong.Columns.Add("cuoiky");
        }
        public void dailyReport(DateTime tndate, DateTime dndate)
        {
            /*****************************TÀI SẢN******************************/
            this.TaiSan.Rows.Clear();
            StockWD wd = new StockWD(this.MaTk,this.Type);
            wd.TN = tndate;
            wd.DN = dndate;
            DataTable tienhiencotn = wd.RnTienHienCotn;//PreviousBalance  
            double tienhicotn = 0;
            foreach (DataRow r in tienhiencotn.Rows)
            {

                tienhicotn += double.Parse(r["PreviousBalance"].ToString());
                
            }
            DataTable tienhiencodn = wd.RnTienHienCodn;
            double tienhicodn = 0;
            foreach (DataRow r in tienhiencodn.Rows)
            {

                tienhicodn += double.Parse(r["PreviousBalance"].ToString());

            }

            this.TaiSan.Rows.Add(new object[] { "Tiền hiện có", tienhicotn, tienhicodn });
            
            //this.TaiSan.Rows.Add(new object[] { "Tiền hiện có", 0,  });
            OnlinePrice price = new OnlinePrice(this.MaTk,this.Type);
            price.TN = tndate;
            price.DN = dndate;
            /*
             * MatchedVolume
             *  OrderSide
             *   MatchedValue
             */
            DataTable ckchove_tn = price.RnCKChoVe_tn;//[StockCode],[MatchedVolume], [OrderSide], MatchedValue
            double chungkhoanchovetn = 0;
            foreach(DataRow r in ckchove_tn.Rows)
            {
                if (r["OrderSide"].ToString() == "S")
                {
                    chungkhoanchovetn += double.Parse(r["MatchedValue"].ToString());
                }
            }

            DataTable ckchove_dn = price.RnCKChoVe_dn;//[StockCode],[MatchedVolume], [OrderSide],MatchedValue
            double chungkhoanchovedn = 0;
            foreach (DataRow r in ckchove_dn.Rows)
            {
                if (r["OrderSide"].ToString() == "S")
                {
                    chungkhoanchovedn += double.Parse(r["MatchedValue"].ToString());
                }
            }


            this.TaiSan.Rows.Add(new object[] { "Tiền bán chứng khoán chờ về", chungkhoanchovetn, chungkhoanchovedn });
            DataTable ckhiencotn = price.RnCK_HienCo_tn;//AccountId,StockCode, Quantity
            DataTable giacktn = price.RnGiaCKtn;//StockCode,PriceStock
            DataTable giackdn = price.RnGiaCKdn;//StockCode,PriceStock
            double ckhiencotnval = 0;
            foreach (DataRow r in ckhiencotn.Rows)
            {
                DataRow[] rs=giacktn.Select("StockCode='"+r["StockCode"]+"'");
                if (rs.Length> 0)
                {
                    DataRow row=rs.First<DataRow>();
                    //string str = row["PriceStock"].ToString();
                    ckhiencotnval += double.Parse(r["Quantity"].ToString()) * (double.Parse(row["PriceStock"].ToString()));
                }
            }
            DataTable ckhiencodn = price.RnCK_HienCo_dn;//AccountId,StockCode, Quantity
            double ckhiencodnval = 0;
            foreach (DataRow r in ckhiencodn.Rows)
            {
                DataRow[] rs = giackdn.Select("StockCode='" + r["StockCode"] + "'");
                if (rs.Length > 0)
                {
                    DataRow row = rs.First<DataRow>();
                    ckhiencodnval += double.Parse(r["Quantity"].ToString()) * (double.Parse(row["PriceStock"].ToString()));
                }
            }
            this.TaiSan.Rows.Add(new object[] { "Chứng khoán hiện có", ckhiencotnval, ckhiencodnval });
            
            chungkhoanchovetn = 0;
            foreach (DataRow r in ckchove_tn.Rows)
            {
                if (r["OrderSide"].ToString() == "B")
                {
                    chungkhoanchovetn += double.Parse(r["MatchedValue"].ToString());
                }
            }
            chungkhoanchovedn = 0;
            foreach (DataRow r in ckchove_dn.Rows)
            {
                if (r["OrderSide"].ToString() == "B")
                {
                    chungkhoanchovedn += double.Parse(r["MatchedValue"].ToString());
                }
            }
            this.TaiSan.Rows.Add(new object[] { "Chứng khoán chờ về", chungkhoanchovetn, chungkhoanchovedn });
            /*****************************NỢ******************************/
            this.No.Rows.Clear();
            StockExchangeData data = new StockExchangeData(this.MaTk,this.Type);
            data.TN = tndate;
            data.DN = dndate;
            DataTable tbNoKyQuyTn = data.RnNoKyQuy_tn;//Balance
            double nokyquytn = 0;
            if (tbNoKyQuyTn.Rows.Count > 0)
            {
                nokyquytn=double.Parse(tbNoKyQuyTn.Rows[0]["Balance"].ToString());
            }
            DataTable tbNoKyQuyDn = data.RnNoKyQuy_dn;//Balance
            double nokyquydn = 0;
            if (tbNoKyQuyDn.Rows.Count > 0)
            {
                nokyquydn = double.Parse(tbNoKyQuyDn.Rows[0]["Balance"].ToString());
            }
            this.No.Rows.Add(new object[] { "Nợ ký quỹ", nokyquytn, nokyquydn });

            DataTable tbNoUngTruocTn = data.RnNoUngTruoc_tn;//ContractId, DateContract,   AccountId, AdvanceAmount,[Status]
            double noungtruoctn = 0;
            foreach(DataRow r in tbNoUngTruocTn.Rows)
            {
                noungtruoctn += double.Parse(r["AdvanceAmount"].ToString());
            }
            DataTable tbNoUngTruocDn = data.RnNoUngTruoc_dn;//ContractId, DateContract,   AccountId, AdvanceAmount,[Status]
            double noungtruocdn = 0;
            foreach (DataRow r in tbNoUngTruocDn.Rows)
            {
                noungtruocdn += double.Parse(r["AdvanceAmount"].ToString());
            }
            this.No.Rows.Add(new object[] { "Nợ ứng trước", noungtruoctn, noungtruocdn });
            DataTable rnNoMuaQuyen_tn = data.RnNoMuaQuyen_tn;//ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status]
            double nomuaquyentn = 0;
            foreach (DataRow r in rnNoMuaQuyen_tn.Rows)
            {
                nomuaquyentn += double.Parse(r["AdvanceAmount"].ToString());
            }
            DataTable rnNoMuaQuyen_dn = data.RnNoMuaQuyen_dn;//ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status]
            double nomuaquyendn = 0;
            foreach (DataRow r in rnNoMuaQuyen_dn.Rows)
            {
                nomuaquyendn += double.Parse(r["AdvanceAmount"].ToString());
            }
            this.No.Rows.Add(new object[] { "Nợ mua quyền", nomuaquyentn, nomuaquyendn });
            //chưa có phần này
            this.No.Rows.Add(new object[] { "Nợ khác", 0, 0 });
            /*****************************TÀI SẢN KHÁC******************************/
            this.TaiSanKhac.Rows.Clear();
            DataTable RnPhatHanhThemtn=price.RnPhatHanhThemtn;//[CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity]
            double phatHanhThemtn = 0;
            foreach (DataRow r in RnPhatHanhThemtn.Rows)
            {
                phatHanhThemtn += double.Parse(r["RegisteredAmount"].ToString());
            }
            DataTable RnPhatHanhThemdn = price.RnPhatHanhThemdn;//[CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity]
            double phatHanhThemdn = 0;
            foreach (DataRow r in RnPhatHanhThemdn.Rows)
            {
                phatHanhThemdn += double.Parse(r["RegisteredAmount"].ToString());
            }
            this.TaiSanKhac.Rows.Add(new object[] { "Mua đã nộp tiền", phatHanhThemtn, phatHanhThemdn });
            DataTable RnCoTuctn = price.RnCoTuctn;//[StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent]
            double rncotuctn_cophieu = 0;
            double rncotuctn_cotuc = 0;
            foreach (DataRow r in RnCoTuctn.Rows)
            {
                if (r["RightType"].ToString() == "S")
                {
                    DataRow[] rs = giacktn.Select("StockCode='" + r["StockCodeCurrent"] + "'");
                    double lookup = 0;
                    if (rs.Length > 0)
                    {
                        DataRow row = rs.First<DataRow>();
                        lookup=(double.Parse(row["PriceStock"].ToString()));
                    }
                    rncotuctn_cophieu += double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * lookup;
                }
                else
                {

                    if (r["RightType"].ToString() == "M")
                    {
                        rncotuctn_cotuc += (double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * 10000)
                            - (double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * 10000*5/100);
                    }
                }
            }

            DataTable RnCoTucdn = price.RnCoTucdn;//[StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent]
            double rncotucdn_cophieu = 0;
            double rncotucdn_cotuc = 0;
            foreach (DataRow r in RnCoTucdn.Rows)
            {
                if (r["RightType"].ToString() == "S")
                {
                    DataRow[] rs = giackdn.Select("StockCode='" + r["StockCodeCurrent"] + "'");
                    double lookup = 0;
                    if (rs.Length > 0)
                    {
                        DataRow row = rs.First<DataRow>();
                        lookup = (double.Parse(row["PriceStock"].ToString()));
                    }
                    rncotucdn_cophieu += double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * lookup;
                }
                else
                {

                    if (r["RightType"].ToString() == "M")
                    {
                        rncotucdn_cotuc += (double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * 10000)
                            - (double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * 10000 * 5 / 100);
                    }
                }
            }
            this.TaiSanKhac.Rows.Add(new object[] { "Cổ phiếu thưởng trở về", rncotuctn_cophieu, rncotucdn_cophieu });
            this.TaiSanKhac.Rows.Add(new object[] { "Cổ tức chờ về", rncotuctn_cotuc, rncotucdn_cotuc });
            /*****************************TÀI SẢN RÒNG******************************/
            //this.TaiSanRong.Rows.Add(new object[] { "Cổ phiếu thưởng trở về", rncotuctn_cophieu, rncotucdn_cophieu });

        }
        public double tongTaiSanTN()
        {
            double sum = 0;
            foreach(DataRow r in TaiSan.Rows){
                sum +=double.Parse( r["dauky"].ToString());
            }
            return sum;
        
        }
        public double tongNoTN()
        {
            double sum = 0;
            foreach (DataRow r in No.Rows)
            {
                sum += double.Parse(r["dauky"].ToString());
            }
            return sum;
        }
        public double tongTaiSanDN()
        {
            double sum = 0;
            foreach (DataRow r in TaiSan.Rows)
            {
                sum += double.Parse(r["cuoiky"].ToString());
            }
            return sum;

        }
        public double tongNoDN()
        {
            double sum = 0;
            foreach (DataRow r in No.Rows)
            {
                sum += double.Parse(r["cuoiky"].ToString());
            }
            return sum;
        }
        public double tongTaiSanKhacTN()
        {
            double sum = 0;
            foreach (DataRow r in TaiSanKhac.Rows)
            {
                sum += double.Parse(r["dauky"].ToString());
            }
            return sum;

        }
        public double tongTaiSanKhacDN()
        {
            double sum = 0;
            foreach (DataRow r in TaiSanKhac.Rows)
            {
                sum += double.Parse(r["cuoiky"].ToString());
            }
            return sum;

        }
        public double taiSanRongTN()
        {
            return tongTaiSanTN() - tongNoTN();
        }
        public double taiSanRongDN()
        {
            return tongTaiSanDN() - tongNoDN();
        }
        public DataTable TaiSan = new DataTable();
        public DataTable No = new DataTable();
        public DataTable TaiSanKhac = new DataTable();
        public string MaTk="";
        //public DataTable TaiSanRong = new DataTable();
        private string tenTaiKhoan = "";
        public bool exisAcc=false;
        private string Type="N";
        public string TenTaiKhoan
        {
            get {
                //=MID('Tai San'!F3,1,3)&"C"&MID('Tai San'!F3,5,6) 094K002555=>094C002555
                if (MaTk.Length < 6)
                {
                    tenTaiKhoan = "Không tìm thấy tài khoản";
                    exisAcc = false;
                    return tenTaiKhoan;
                }
                string matentk=MaTk.Replace(MaTk.Substring(0, 4) , MaTk.Substring(0, 3)+"C");

                string StSQLTenTK = "SELECT [CustomerNameViet] FROM [VFS_Client]  WHERE CustomerId = '" + matentk + "'";
                DataTable info=VSDBConnection.getDataTable(StSQLTenTK,VSDBConnection.CSVSFServices);
                if (info.Rows.Count > 0)
                {
                    tenTaiKhoan = info.Rows[0]["CustomerNameViet"].ToString();
                    exisAcc = true;
                }
                else
                {
                    tenTaiKhoan = "Không tìm thấy tài khoản";
                    exisAcc = false;
                }
                return tenTaiKhoan; }
            set { tenTaiKhoan = value; }
        }
        public DataTable getDataForAYear(DateTime todate)
        {
            DataTable yearly = new DataTable();
            yearly.Columns.Add("ten");
            yearly.Columns.Add("taisan");
            yearly.Columns.Add("no");
            yearly.Columns.Add("taisanrong");
            StockTaiSanReport taisan = new StockTaiSanReport(this.MaTk,this.Type);
            for (int i = 11; i >= 0; i--)
            {
                DateTime time = new DateTime(todate.Year, todate.Month, 1).AddMonths(-i).AddDays(-1);
                taisan.dailyReport(time, time);
                yearly.Rows.Add(new object[] { "Tháng "+ time.Month.ToString("00") + "-" + time.Year, taisan.tongTaiSanTN(), taisan.tongNoTN(), taisan.tongTaiSanTN() - taisan.tongNoTN() });
            }
                return yearly;
        }
    }
}