
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SubmitToApi
{
    public partial class Form1 : Form
    {


        public string UnserInfoEncode { get; set; }
        public string URLGetAllOrder { get; set; }
        private string URLGetAllOrderDetail;
        private string URLGetAllOrderExport;

        public ApiResult apiResult { get; set; }

        public Dictionary<string, string> HeaderAdd { get; set; }
        public Form1()
        {
            InitializeComponent();
            //UserInfo = new Tuple<string, string>("ecount.vn@gmail.com", "Ecount@80841");

            //UnserInfoEncode = "email=ecount.vn%40gmail.com&password=Ecount%4080841";

            UnserInfoEncode = "email=ecount.vn%40gmail.com&password=Ecount%4080841&submit=%C4%90%C4%83ng+nh%E1%BA%ADp";

            URLGetAllOrder = @"https://sellercenter.lazada.vn/order/index/index/filteredStatus/0";
            URLGetAllOrderDetail = @"https://sellercenter.lazada.vn/order/index/query/sectionFilteredStatus/0/originalFilterStatus/0/filteredStatus/0/filteredPrintStatus/all?&sEcho=1&iColumns=13&sColumns=&iDisplayStart=0&iDisplayLength=10&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2&mDataProp_3=3&mDataProp_4=4&mDataProp_5=5&mDataProp_6=6&mDataProp_7=7&mDataProp_8=8&mDataProp_9=9&mDataProp_10=10&mDataProp_11=11&mDataProp_12=12&sSearch=&bRegex=false&sSearch_0=&bRegex_0=false&bSearchable_0=true&sSearch_1=&bRegex_1=false&bSearchable_1=true&sSearch_2=&bRegex_2=false&bSearchable_2=true&sSearch_3=&bRegex_3=false&bSearchable_3=true&sSearch_4=&bRegex_4=false&bSearchable_4=true&sSearch_5=&bRegex_5=false&bSearchable_5=true&sSearch_6=&bRegex_6=false&bSearchable_6=true&sSearch_7=&bRegex_7=false&bSearchable_7=true&sSearch_8=&bRegex_8=false&bSearchable_8=true&sSearch_9=&bRegex_9=false&bSearchable_9=true&sSearch_10=&bRegex_10=false&bSearchable_10=true&sSearch_11=&bRegex_11=false&bSearchable_11=true&sSearch_12=&bRegex_12=false&bSearchable_12=true&iSortCol_0=6&sSortDir_0=DESC&iSortingCols=1&bSortable_0=false&bSortable_1=false&bSortable_2=false&bSortable_3=false&bSortable_4=true&bSortable_5=true&bSortable_6=true&bSortable_7=true&bSortable_8=true&bSortable_9=false&bSortable_10=false&bSortable_11=false&bSortable_12=false";

            URLGetAllOrderExport = @"https://sellercenter.lazada.vn/export/index/query/filteredStatus/0/export-action/Order%2COrderToShip%2COrderDocumentsAsPdf?sEcho=1&iColumns=6&sColumns=&iDisplayStart=0&iDisplayLength=5&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2&mDataProp_3=3&mDataProp_4=4&mDataProp_5=5&sSearch=&bRegex=false&sSearch_0=&bRegex_0=false&bSearchable_0=true&sSearch_1=&bRegex_1=false&bSearchable_1=true&sSearch_2=&bRegex_2=false&bSearchable_2=true&sSearch_3=&bRegex_3=false&bSearchable_3=true&sSearch_4=&bRegex_4=false&bSearchable_4=true&sSearch_5=&bRegex_5=false&bSearchable_5=true&iSortCol_0=0&sSortDir_0=desc&iSortingCols=1&bSortable_0=true&bSortable_1=true&bSortable_2=true&bSortable_3=true&bSortable_4=true&bSortable_5=true&_=1470622115288";

            HeaderAdd = new Dictionary<string, string>() 
            {
                {"X-Requested-With",@"XMLHttpRequest"}                
            };


        }
        public string URLAPI
        {
            get
            {
                return DataPOST.Text;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();

            var apiResult = WebReq.GetWebRequest(this.URL.Text, this.METHOL.Text, this.DataPOST.Text, UTF8Encoding.UTF8, null, null, 240000);

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = apiResult.Html;

        }

        private void BoxResult(ApiResult AllOrderDetail)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = AllOrderDetail.Html;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();
            apiResult = WebReq.GetWebRequest(@"https://sellercenter.lazada.vn/user/auth/login", "POST", this.UnserInfoEncode, UTF8Encoding.UTF8, null, cookieContainer, 240000);

            //string refer = "https://sellercenter.lazada.vn/order/index/index/filteredStatus/0";
            //var AllOrderDetail = WebReq.GetWebRequest(this.URLGetAllOrderDetail, "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, HeaderAdd, apiResult.cookieContainer, 240000, refer);

            //var AllOrderExport = WebReq.GetWebRequest(this.URLGetAllOrderExport, "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, HeaderAdd, apiResult.cookieContainer, 240000, refer);

            this.BoxResult(apiResult);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            URL.Text = ConfigurationManager.AppSettings["DefaultURL"];
            DataPOST.Text = ConfigurationManager.AppSettings["DefaultDataPost"];
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var refer = string.Empty;
            System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();

            //var ManaGetCookieToLogin = WebReq.GetWebRequest(@"https://sellercenter.lazada.vn/?/login", "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, null, cookieContainer, 240000);


            var apiResultLogin = WebReq.GetWebRequest(@"https://sellercenter.lazada.vn/user/auth/login", "POST", this.UnserInfoEncode, UTF8Encoding.UTF8, null, cookieContainer, 240000);


            //var ManaProduct = WebReq.GetWebRequest(@"https://sellercenter.lazada.vn/order", "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, null, apiResultLogin.cookieContainer, 240000);


            //var stringtest = GetCSRFKey(ManaProduct.Html);
            //var Websubmit = WebReq.GetWebRequest("https://sellercenter.lazada.vn/product/index/change-price", EnumMethol.POST.ToString(), "product_id=2326258&csrf=" + stringtest + "&price_val=510%2C000.00&submit=Save", UTF8Encoding.UTF8, HeaderAdd, apiResultLogin.cookieContainer, 240000, null, "Price");
            string URLLinkRefer = "https://sellercenter.lazada.vn/order";
            string getReson = @"https://sellercenter.lazada.vn/order/index/processing?bulkOrderData=%7B%22orders%22%3A%5B%7B%22orderId%22%3A6470610%2C%22orderItems%22%3A%5B%5D%7D%5D%2C%22params%22%3A%7B%22startDate%22%3Anull%2C%22endDate%22%3Anull%2C%22prevSearch%22%3A%22%22%2C%22filteredStatus%22%3A1%2C%22nextStatus%22%3A%22canceled%22%2C%22isBulk%22%3Atrue%2C%22orderItemProcessed%22%3A1%7D%7D&action_name=canceled";
            var getReason = WebReq.GetWebRequest(getReson, "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, HeaderAdd, apiResultLogin.cookieContainer, 240000, URLLinkRefer);


            //
            if (true) {

            }

            this.BoxResult(getReason);
        }

        private string GetCSRFKey(string htmlString)
        {
            Match match = Regex.Match(htmlString, @"csrf.*appKey\W+\w+");
            if (match.Success) {

                var testjksfl = Regex.Split(match.ToString(), @"\W+");
                return testjksfl[1];
            }

            return "";
        }

        public string UrlEncode(string encodeStr)
        {
            string erg = null;
            erg = encodeStr;
            erg = Strings.Replace(erg, "%", Strings.Chr(1).ToString());
            erg = Strings.Replace(erg, "+", Strings.Chr(2).ToString());
            IList<int> ArrayIgnore = new List<int>
            {
                37,43,45,46,48,49,50,51,52,53,54,55,56,57,
                65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,
                95,
                97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,
                126
            };

            for (int i = 0; i <= 255; i++) {
                if (!ArrayIgnore.Contains(i)) {
                    switch (i) {
                        // *** Allowed 'regular' characters                    
                        case 1:
                            // *** Replace original %
                            erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "%25");
                            break;
                        case 2:
                            // *** Replace original +
                            erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "%2B");
                            break;
                        case 32:
                            erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "+");
                            break;
                        //case 3: // TODO: to 15
                        //    erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "%0" + Conversion.Hex(i));
                        //    break;
                        default: {
                                if ((i >= 3 && i <= 15))
                                    erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "%0" + Conversion.Hex(i));
                                else
                                    erg = Strings.Replace(erg, Strings.Chr(i).ToString(), "%" + Conversion.Hex(i));

                                break;
                            }
                    }
                }

            }
            return erg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var stringdecode = @"";

            var fjdksaf = UrlEncode(stringdecode);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string URL = @"https://sellercenter-api.lazada.vn/?Action=GetOrders&CreatedAfter=2014-02-25T23%3A46%3A11%2B00%3A00&Format=JSON&Timestamp=2016-08-16T10%3A34%3A31%2B07%3A00&UserID=ecount.vn%40gmail.com&Version=1.0&Signature=6acd1646fd71cd3a052dc79e182cb1adc237bc237c95c9929ffd7be69e4961b0";
            var getReason = WebReq.GetWebRequest(URL, "GET", this.UnserInfoEncode, UTF8Encoding.UTF8, null, null);
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };

            var order = JsonConvert.DeserializeObject<SubmitToApi.EntityOrders.RootObject>(getReason.Html);

            this.BoxResult(getReason);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var stringRegex = @"orderItems.*]";
            var sjfkdls = "orderItems:[8118162]},{orderId:6808839,orderItems:[8118161]},{orderId:6808837,orderItems:[8118158]},{orderId:6808836,orderItems:[8118157]}]";

            var replatetest = Regex.Matches(sjfkdls, @"\[(\d+)\]");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region [Appconfig]

            string domainName = AppHelper.DomainName;
            string subDomain = AppHelper.SubDomain;
            string com_code = AppHelper.Com_code;
            string idLogin = AppHelper.IdLogin;
            string password = AppHelper.Password;
            string selHour = AppHelper.SelHour;
            string selMin = AppHelper.SelMin;
            string selAm = AppHelper.SelAm;

            string hidSelDate = DateTime.Now.ToString("yyyy-MM-dd");
            CookieContainer cookieAuthor = new System.Net.CookieContainer();

            #endregion

            #region [Login to Ecount system]

            // Login Eccount
            bool resultLogin = LoginSystem(subDomain, com_code, idLogin, password, cookieAuthor);
            var APILoginDto = new ApiResult();

            // Sumit to Clock In
            if (resultLogin == true) {
                string AlertAutoLogin = "Login thành công <br />";
                APILoginDto = AutoClock_In(domainName, subDomain, selHour, selMin, selAm, hidSelDate, cookieAuthor);

                if (APILoginDto.Html.Contains("EGM014M")) {
                    AlertAutoLogin += string.Format( "Clock in thành công tại {0} : {1} : {2} : {3}", selHour , selMin, selAm, hidSelDate);
                    APILoginDto.Html = AlertAutoLogin;
                }
                else {
                    APILoginDto.Html = AlertAutoLogin + "Clock In không thành công";
                }
                this.BoxResult(APILoginDto);
            }
            else {
                APILoginDto.Html = "Login Fail nè";
                this.BoxResult(APILoginDto);

            }

            #endregion
        }

        private static ApiResult AutoClock_In(string domainName, string subDomain, string selHour, string selMin, string selAm, string hidSelDate, CookieContainer cookieAuthor)
        {
            var sessionServer = cookieAuthor.GetCookies(new Uri(domainName))[0].Value.Split('=')[0];
            var dataPostClockin = String.Format("__EVENTTARGET=lnkSave&__EVENTARGUMENT=&txtPlace=Office&selHour={0}&selMin={1}&selAm={2}&txtComment=&hfg_type=&hidUserId=PHI&hidEditFlag=I&hidSelDate={3}&hidTabgubun=0&hidSeq=&hidLastSeq=&hidPlace=&hidComment=&hidStartOrEnd=S&ChangeDate=&USER_ID=&hidSiteCd=00&hidNoData=Y&hidLastSOrE=", selHour, selMin, selAm, hidSelDate);
            var refOpenmarketdsss = string.Format("http://{0}/ECMain/EGM/EGM015M.aspx?ec_req_sid={1}", subDomain, sessionServer);
            var apiAPILogin = WebReq.GetWebRequest(string.Format("http://{0}/ECMain/EGM/EGM015M.aspx?ec_req_sid={1}", subDomain, sessionServer), "POST", dataPostClockin, UTF8Encoding.UTF8, null, cookieAuthor, 240000, refOpenmarketdsss);
            return apiAPILogin;
        }

        private static bool LoginSystem(string subDomain, string com_code, string idLogin, string password, CookieContainer cookieAuthor)
        {            
            string referLogin = string.Format("https://{0}/ECMain/Login.aspx", subDomain);
            string datapostLogin = string.Format("com_code={0}&id={1}&not-id=&passwd={2}&language_select=en-US&lan_type=en-US&lan_type_Ect=en-US&login_type=0&login_gubun=1&access_site=ECOUNT&access_type=B&server_name=Z-LOGIN-012212&process_ing=Y", com_code, idLogin, password);
            var loginResultString = WebReq.GetWebRequest(string.Format("https://{0}/ECMAIN/LoginResult.aspx", subDomain), "POST", datapostLogin, UTF8Encoding.UTF8, null, cookieAuthor, 240000, referLogin);

            if (loginResultString.Html.Contains("ECP040M"))
                return true;
            else                
                return false;
        }

    }


}
