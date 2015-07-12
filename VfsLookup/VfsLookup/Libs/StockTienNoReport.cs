using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class StockTienNoReport
    {
        public StockTienNoReport(string MaTk,string Type)
        {
            this.Type=Type;
            this.MaTk=MaTk;
            this.TienNhan.Columns.Add("ten");
            this.TienNhan.Columns.Add("no");

            this.TienChi.Columns.Add("ten");
            this.TienChi.Columns.Add("no");

            this.TienCho.Columns.Add("ten");
            this.TienCho.Columns.Add("no");

        }
        public DataTable TienNhan=new DataTable();
        public DataTable TienChi = new DataTable();
        public DataTable TienCho = new DataTable();
        double tinhTrangDauKy;

        public double TinhTrangDauKy
        {
            get { 
                
                return tinhTrangDauKy; }
            set { tinhTrangDauKy = value; }
        }
        double tinhTrangCuoiKy;
        public string MaTk;

        public double TinhTrangCuoiKy
        {
            get { return tinhTrangCuoiKy; }
            set { tinhTrangCuoiKy = value; }
        }
        public void makeReport(DateTime tn, DateTime dn)
        {
            StockTienVaNo tienno = new StockTienVaNo(this.MaTk,this.Type);
            tienno.TN = tn;
            tienno.DN = dn;
            /***********************tinh trang dau ky***********************************/
            DataTable tinhtrangtiendauky = tienno.RnTinhTrangTienDauKy;//PreviousBalance
            this.TinhTrangDauKy = 0;
            foreach (DataRow r in tinhtrangtiendauky.Rows)
            {
                this.TinhTrangDauKy += double.Parse(r["PreviousBalance"].ToString());


            }
            /***********************Tiền Nhận***************************************/
            double tienchiTTPhiGiaodichck = 0;
            double tamnopthue = 0;
           
            DataTable muabanck = tienno.RnMuaBanCK;//StockCode,OrderDate,[MatchedValue] GiaTriMua,[MatchedValue] * FeeRate as MFR , [MatchedValue] * 0.0001 TaxValue ,[OrderSide]
            double tiennhanck = 0;
            foreach (DataRow r in muabanck.Rows)
            { 
                if(r["OrderSide"].ToString()=="S"){
                    double d=double.Parse(r["GiaTriMua"].ToString());
                    tiennhanck += double.Parse(r["GiaTriMua"].ToString());
                    
                    tienchiTTPhiGiaodichck += double.Parse(r["MFR"].ToString());
                    tamnopthue += double.Parse(r["TaxValue"].ToString());
                }
                
                
            }
            DataTable nhancotuc=tienno.RnNhanCoTuc;//NCT
            double nhancotucint = 0;
            if (nhancotuc.Rows.Count > 0)
            {
                nhancotucint =double.Parse( nhancotuc.Rows[0]["NCT"].ToString());
            }
            DataTable tranokyquy = tienno.RnTraNoKyQuy;//1.ContractId, LogUser AS 2.UserCreate, EffectiveOnDate AS 3.DateContract, 
                                                       //EffectiveToDate AS 4.PaymentDate,LoanType AS 5.ContractType, 6.CustomerId, 
                                                        //7.AccountId, Amount AS 8.AdvanceAmount,(AmountCalInterest) AS 9.DebtValue,(InterestPayNow) AS 
                                                        //10.InterestValue,11.[Status] 
            double phatvaykyquyint = 0;
            foreach (DataRow r in tranokyquy.Rows)
            { 
                if(r["Status"].ToString()=="B"){
                    phatvaykyquyint +=double.Parse( r["DebtValue"].ToString());
                }
                    
            }
            DataTable tranoungtruoc = tienno.RnTraNoUngtruoc;//1.ContractId, 2.UserCreate,3.DateContract,  4.PaymentDate,5.ContractType, 6.CustomerId, 
                                                             //7.AccountId, 8.AdvanceAmount,9.AdvanceAmount,10.AdvanceFee,11.[Status]
            double phatvayungtruocint = 0;
            double tralai = 0;
            foreach (DataRow r in tranoungtruoc.Rows)
            {

                phatvayungtruocint +=double.Parse(r["AdvanceAmount"].ToString());
                tralai += double.Parse(r["AdvanceFee"].ToString());
            }
            DataTable tranomuaquyen = tienno.RnTraNoMuaQuyen;//1.ContractId, 2.UserCreate,3.DateContract,  4.PaymentDate,5.ContractType, 6.CustomerId, 
                                                            //7.AccountId, 8.AdvanceAmount,9.AdvanceAmount,10.AdvanceFee,11.[Status]
            double phatvaymuaquyenint = 0;
            foreach (DataRow r in tranomuaquyen.Rows)
            {

                phatvaymuaquyenint += double.Parse(r["AdvanceAmount"].ToString());

            }
            DataTable tiennopvao = tienno.RnTienNopVao;//sumcreadit
            double tiennopvaoint = 0;
            if (tiennopvao.Rows.Count > 0)
            {
                string s = tiennopvao.Rows[0]["sumcreadit"].ToString();
                if (s!="")
                tiennopvaoint = double.Parse(s);
            }
            DataTable tinhtrangdauky = tienno.RnTinhTrangDauKy;//TransactionDate, AccountId, Amount, DebitOrCredit, Description Description
            double tiennhankhac = 0;
            double tienchikhac = 0;
            foreach (DataRow r in tinhtrangdauky.Rows)
            {
                if (r["DebitOrCredit"].ToString() == "C")
                {
                    tiennhankhac += double.Parse(r["Amount"].ToString());
                }
                if (r["DebitOrCredit"].ToString() == "D")
                {
                    tienchikhac += double.Parse(r["Amount"].ToString());
                }
            }
            tiennhankhac = tiennhankhac - tiennhanck - nhancotucint - phatvaykyquyint - phatvayungtruocint - phatvaymuaquyenint - tiennopvaoint;



            /***********************Tiền Chi***************************************/
            DataTable muabanck1 = tienno.RnMuaBanCK1;//[MatchedValue] GiaTriMua,FeeValue Fee, TaxValue Tax,[OrderSide]  
            double tttienmuack = 0;
            double tienchiTTPhiGiaodichck1 = 0;
            foreach (DataRow r in muabanck1.Rows)
            {
                if (r["OrderSide"].ToString() == "B")
                {
                    tttienmuack += double.Parse(r["GiaTriMua"].ToString());
                    tienchiTTPhiGiaodichck1 += double.Parse(r["Fee"].ToString());
                }
            }
            DataTable tranokyquyfix = tienno.RnTraNoKyQuyFix;//ContractId,LogDate,AmountCalInterest,InterestPayNow
            double tranokyquyint = 0;
            foreach (DataRow r in tranokyquyfix.Rows)
            {

                tranokyquyint += double.Parse(r["AmountCalInterest"].ToString());
                tralai += double.Parse(r["InterestPayNow"].ToString());

            }
            DataTable tranoungtruocfix = tienno.RnTraNoUngtruocFix;//ContractId,  PaymentDate,AdvanceAmount,[Status]
            double tranoungtruocint = 0;
            foreach (DataRow r in tranoungtruocfix.Rows)
            {

                tranoungtruocint += double.Parse(r["AdvanceAmount"].ToString());

            }
            DataTable tranomuaquyenfix = tienno.RnTraNoMuaQuyenFix;//ContractId,AccountId,AdvanceAmount,AdvanceFee
            double tranomuaquyenint = 0;
            foreach (DataRow r in tranomuaquyenfix.Rows)
            {

                tranomuaquyenint += double.Parse(r["AdvanceAmount"].ToString());
                tralai+=double.Parse(r["AdvanceFee"].ToString());

            }
            DataTable tienrut = tienno.RnTienRut;//SumDebitAmount
            double tienrutint = 0;
            if (tienrut.Rows.Count > 0)
            {
                string s = tienrut.Rows[0]["SumDebitAmount"].ToString();
                if(s!="")
                tienrutint = double.Parse(s);
            }
            tienchikhac = tienchikhac - tienrutint - (tralai) - tranomuaquyenint - tranoungtruocint - tranokyquyint - tamnopthue * 10 - (tienchiTTPhiGiaodichck1 + tienchiTTPhiGiaodichck) - tttienmuack;
            /***********************Tiền Chờ về***************************************/
            DataTable tienbanckchove = tienno.RnTienBanCKChoVe;//[MatchedValue] GiaTriMua,FeeValue Fee, TaxValue Tax,[OrderSide]
            double tienbanckchoveint = 0;
            foreach (DataRow r in tienbanckchove.Rows)
            {
                if (r["OrderSide"].ToString()=="S")

                    tienbanckchoveint += double.Parse(r["GiaTriMua"].ToString());

            }
            DataTable chotucchove = tienno.RnCoTucChoVe;//[StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent]
            double chotucchoveint = 0;
            foreach (DataRow r in chotucchove.Rows)
            {

                chotucchoveint += 
                    double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString())*10000
                    - double.Parse(r["RateA"].ToString()) / double.Parse(r["RateB"].ToString()) * double.Parse(r["QuantityCurrent"].ToString()) * 10000*5/100
                    
                    ;

            }

            /***********************************************************************/
            /********************************Tiền Nhận******************************/
            /***********************************************************************/
            this.TienNhan.Rows.Add(new object[] { "Nhận tiền bán chứng khoán", tiennhanck });
            this.TienNhan.Rows.Add(new object[] { "Nhận cổ tức", nhancotucint });
            this.TienNhan.Rows.Add(new object[] { "Nhận phát vay ký quỹ", phatvaykyquyint });
            this.TienNhan.Rows.Add(new object[] { "Nhận phát vay ứng trước", phatvayungtruocint });
            this.TienNhan.Rows.Add(new object[] { "Nhận phát vay mua quyền", phatvaymuaquyenint });
            this.TienNhan.Rows.Add(new object[] { "Tiền nộp vào tài khoản", tiennopvaoint });
            this.TienNhan.Rows.Add(new object[] { "Tiền nhận khác", tiennhankhac });
            /***********************Tiền Chi***************************************/
            this.TienChi.Rows.Add(new object[] { "Thanh toán tiền mua chứng khoán", tttienmuack });
            this.TienChi.Rows.Add(new object[] { "Thanh toán phí giao dịch", tienchiTTPhiGiaodichck1+tienchiTTPhiGiaodichck });
            this.TienChi.Rows.Add(new object[] { "Tạm nộp thuế",tamnopthue*10 });
            this.TienChi.Rows.Add(new object[] { "Trả nợ ký quỹ", tranokyquyint });
            this.TienChi.Rows.Add(new object[] { "Trả nợ ứng trước", tranoungtruocint });
            this.TienChi.Rows.Add(new object[] { "Trả nợ mua quyền", tranomuaquyenint });
            this.TienChi.Rows.Add(new object[] { "Trả lãi",  tralai });
            this.TienChi.Rows.Add(new object[] { "Tiền rút, chuyển khoản ra ngoài", tienrutint });
            this.TienChi.Rows.Add(new object[] { "Tiền chi khác", tienchikhac });
            /***********************Tiền Chờ về***************************************/
            this.TienCho.Rows.Add(new object[] { "Tiền bán chứng khoán chờ về", tienbanckchoveint });
            this.TienCho.Rows.Add(new object[] { "Cổ tức chờ về", chotucchoveint });
        }
        String tenTaiKhoan;
        public bool exisAcc;
        private string Type="N";

        public String TenTaiKhoan
        {


            get
            {
                //=MID('Tai San'!F3,1,3)&"C"&MID('Tai San'!F3,5,6) 094K002555=>094C002555
                if (MaTk.Length < 6)
                {
                    tenTaiKhoan = "Không tìm thấy tài khoản";
                    exisAcc = false;
                    return tenTaiKhoan;
                }
                string matentk = MaTk.Replace(MaTk.Substring(0, 4), MaTk.Substring(0, 3) + "C");

                string StSQLTenTK = "SELECT [CustomerNameViet] FROM [VFS_Client]  WHERE CustomerId = '" + matentk + "'";
                DataTable info = VSDBConnection.getDataTable(StSQLTenTK, VSDBConnection.CSVSFServices);
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
                return tenTaiKhoan;
            }
        }

        internal double tongDauKy()
        {
            ////chua biet xu ly
            return this.TinhTrangDauKy;
        }

        internal double tongCuoiKy()
        {
            return tongDauKy() + tongNhan() - tongChi();
        }

        internal double tongNhan()
        {
            double sum = 0;
            foreach(DataRow r in this.TienNhan.Rows)
            {
                sum += double.Parse(r["no"].ToString());
            }
            return sum;
        }

        internal double tongChi()
        {
            double sum = 0;
            foreach (DataRow r in this.TienChi.Rows)
            {
                sum += double.Parse(r["no"].ToString());
            }
            return sum;
        }

        internal double tongCho()
        {
            double sum = 0;
            foreach (DataRow r in this.TienCho.Rows)
            {
                sum += double.Parse(r["no"].ToString());
            }
            return sum;
        }
    }
}
