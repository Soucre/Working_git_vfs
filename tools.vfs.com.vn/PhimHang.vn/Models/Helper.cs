using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PhimHang.Models
{
    public static class Helper
    {
        public static string MD5Hash(string passwordInput)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(passwordInput);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X1"));
            }
            return sb.ToString();
        }
        public static string RemoveHtmlTag(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static void SetCookieOfVIP()
        {
            var FacebookLogin = new HttpCookie("AccountType", "VIP");
            FacebookLogin.Expires.AddDays(2);
            if (FacebookLogin == null)
            {
                HttpContext.Current.Response.Cookies.Add(FacebookLogin);
            }
            else
            {
                HttpContext.Current.Response.SetCookie(FacebookLogin);
            }

        }

        public static void ReleaseCookieOfFace()
        {
            if (HttpContext.Current.Request.Cookies["AccountType"] != null)
            {
                var user = new HttpCookie("AccountType")
                {
                    Expires = DateTime.Now.AddDays(-2),
                    Value = null
                };
                HttpContext.Current.Response.Cookies.Add(user);
            }
        }
    }
}