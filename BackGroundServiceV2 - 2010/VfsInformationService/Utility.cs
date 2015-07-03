using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using log4net;
using VfsInformationService.SBSWebService;
using System.Text.RegularExpressions;


namespace VfsCustomerInformationServices
{
    public static class Ultility
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string[] VietNamChar = new string[]
	    {
	        "aAeEoOuUiIdDyY",
	        "áàạảãâấầậẩẫăắằặẳẵ",
	        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
	        "éèẹẻẽêếềệểễ",
	        "ÉÈẸẺẼÊẾỀỆỂỄ",
	        "óòọỏõôốồộổỗơớờợởỡ",
	        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
	        "úùụủũưứừựửữ",
	        "ÚÙỤỦŨƯỨỪỰỬỮ",
	       "íìịỉĩ",
	        "ÍÌỊỈĨ",
	        "đ",
	        "Đ",
	        "ýỳỵỷỹ",
	        "ÝỲỴỶỸ"
	    };

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

            //return DateTime.Parse(dateString);
            //return Convert.ToDateTime(dateString, datefomatProvider);
        }

        public static void Info(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Info(message);
            }
        }

        public static void Info(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Info(message, ex);
            }
        }

        public static void Error(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Error(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }

        public static string CutAddressHead(string source)
        {
            string s = source;
            if (s.Length <= 160) return s;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = temp.Substring(0, pos);
            return s;
        }

        public static string CutAddressEnd(string source)
        {
            string s = source;
            if (s.Length <= 160) return string.Empty;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = s.Substring(pos + 1, ((s.Length - 1) - pos));
            return s;
        }

        public static string RemoveSound(string str)
        {             
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }

        public static PorscheCredentials GetCrendentials(string userPorcheGateway, string passPorcheGateway, string userSbs, string passSbs)
        {
            PorscheCredentials porscheCredentials = new PorscheCredentials();

            string UserPorcheGateway = userPorcheGateway;
            string PassPorcheGateway = passPorcheGateway;
            string UserSbs = userSbs;
            string PassSbs = passSbs;
            porscheCredentials.GatewayPassword = PassPorcheGateway;
            porscheCredentials.GatewayUsername = UserPorcheGateway;
            porscheCredentials.SBSUser = new PorscheCredentialsUser();
            porscheCredentials.SBSUser.UserName = UserSbs;
            porscheCredentials.SBSUser.Password = PassSbs;
            porscheCredentials.ClientInfor = new ClientInformation();
            porscheCredentials.ClientInfor.ClientIp = "192.168.1.2";
            porscheCredentials.ClientInfor.ClientName = "giangum";
            porscheCredentials.RequestType = "BackOffice";
            return porscheCredentials;
        }

        public static string ReplaceAsciiToUniCode(string input)
        {
            string text = input;

            MatchCollection matchColl = Regex.Matches(text, @"&#[a-z]*[0-9]*;");
            string result1;
            result1 = text;
            int i = 0;

            Match match = Regex.Match(text, @"&#[a-z]*[0-9]*;");
            while (match.Success)
            {
                string replacingString = char.ConvertFromUtf32(Convert.ToInt32(match.Value.Trim().Replace("&", "").Replace("#", "").Replace(";", "")));
                result1 = result1.Replace(match.ToString(), replacingString);
                match = match.NextMatch();
                i++;
            }
            return result1;
        }
    }
}
