using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnhCop.AutoLib
{
    public class RobotAuto : IDisposable
    {
        #region [Public Properties]

        public string DomainName { get; set; }
        public string SubDomain { get; set; }
        public string Com_code { get; set; }
        public string IdLogin { get; set; }
        public string Password { get; set; }
        public string InSelHour { get; set; }
        public string InSelMin { get; set; }
        public string InSelAm { get; set; }
        public string HidSelDate { get; set; }
        
        //------------------------------------
        
        //public string OutSelHour { get; set; }
        //public string OutSelMin { get; set; }     
        //public string OutSelAm { get; set; }

        //------------------------------------

        private CookieContainer CookieAuthor = new CookieContainer();
        #endregion

        #region [Dispose]
        // Implement temp from Dispose class
        public void Dispose()
        {
            // dispose here
        }

        #endregion

        

        public string SumitClockOut()
        {
            return "Đang phát triển";
        }
        private ApiResult AutoClock_In()
        {
            var sessionServer = this.CookieAuthor.GetCookies(new Uri(this.DomainName))[0].Value.Split('=')[0];
            var dataPostClockin = String.Format("__EVENTTARGET=lnkSave&__EVENTARGUMENT=&txtPlace=Office&selHour={0}&selMin={1}&selAm={2}&txtComment=&hfg_type=&hidUserId={3}&hidEditFlag=I&hidSelDate={4}&hidTabgubun=0&hidSeq=&hidLastSeq=&hidPlace=&hidComment=&hidStartOrEnd=S&ChangeDate=&USER_ID=&hidSiteCd=00&hidNoData=Y&hidLastSOrE=", this.InSelHour, this.InSelMin, this.InSelAm, this.IdLogin, this.HidSelDate);
            var refOpenmarketdsss = string.Format("http://{0}/ECMain/EGM/EGM015M.aspx?ec_req_sid={1}", this.SubDomain, sessionServer);
            var apiAPILogin = WebReq.GetWebRequest(string.Format("http://{0}/ECMain/EGM/EGM015M.aspx?ec_req_sid={1}", this.SubDomain, sessionServer), "POST", dataPostClockin, UTF8Encoding.UTF8, null, this.CookieAuthor, 240000, refOpenmarketdsss);
            return apiAPILogin;
        }
       
        private bool LoginSystem()
        {
            string referLogin = string.Format("https://{0}/ECMain/Login.aspx", this.SubDomain);
            string datapostLogin = string.Format("com_code={0}&id={1}&not-id=&passwd={2}&language_select=en-US&lan_type=en-US&lan_type_Ect=en-US&login_type=0&login_gubun=1&access_site=ECOUNT&access_type=B&server_name=Z-LOGIN-012212&process_ing=Y", this.Com_code, this.IdLogin, this.Password);
            var loginResultString = WebReq.GetWebRequest(string.Format("https://{0}/ECMAIN/LoginResult.aspx", this.SubDomain), "POST", datapostLogin, UTF8Encoding.UTF8, null, this.CookieAuthor, 240000, referLogin);

            if (loginResultString.Html.Contains("ECP040M") || loginResultString.Html.Contains("EMT001M") || loginResultString.Html.Contains("ECL003M") || loginResultString.Html.Contains("ECP200P_08"))
                return true;
            else
                return false;
        }

        private bool IsCheckClockInExist()
        {
            var sessionServer = this.CookieAuthor.GetCookies(new Uri(this.DomainName))[0].Value.Split('=')[0];
            var dataPostCheck = String.Format("hidFavSeq=&mm={0}&dd={1}&yy={2}&HiddenField1=&hidUserId={3}&hidSelDate={4}&hidTabGubun=0&hidData=",
                                                                DateTime.Now.ToString("MM"), "01", DateTime.Now.ToString("yyyy"), this.IdLogin, this.HidSelDate);            
            var refOpenmarketd = string.Format("http://{0}/ECMain/EGM/EGM014M.aspx?ec_req_sid={1}", this.SubDomain, sessionServer);
            var apiAPICheck = WebReq.GetWebRequest(string.Format("http://{0}/ECMain/EGM/EGM015M.aspx?ec_req_sid={1}", this.SubDomain, sessionServer), "POST", dataPostCheck, UTF8Encoding.UTF8, null, this.CookieAuthor, 240000, refOpenmarketd);
            if (apiAPICheck.Html.Contains("")) {
                
            }
            return true;
        }

        public string SumitClockIn()
        {

            #region [Appconfig]

            //CookieContainer cookieAuthor = new System.Net.CookieContainer();

            #endregion

            #region [Login to Ecount system]

            // Login Eccount
            bool resultLogin = LoginSystem();
            var APILoginDto = new ApiResult();
            string AlertAutoLogin = "";
            // login sucessed
            if (resultLogin == true) {
                if (IsCheckClockInExist() == false) return "Da ton tai Clock in";

                AlertAutoLogin = "Login thành công \n";
                APILoginDto = AutoClock_In();

                if (APILoginDto.Html.Contains("EGM014M")) {
                    AlertAutoLogin += string.Format("Clock in sucessed {0} : {1} : {2} : {3}", InSelHour, InSelMin, InSelAm, HidSelDate);
                }
                else {
                    AlertAutoLogin += "Clock In không thành công";
                }
                return AlertAutoLogin;
            }
            else {
                AlertAutoLogin += "Login Fail nè";
            }

            return AlertAutoLogin;
            #endregion
        }
    }
}
