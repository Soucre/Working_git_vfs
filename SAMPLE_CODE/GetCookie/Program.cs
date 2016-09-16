using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GetCookie
{
    class Program
    {
        static void Main(string[] args) {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.GetAsync("https://sellercenter.lazada.vn/?/login").Result;

            Uri uri = new Uri("https://sellercenter.lazada.vn/?/login");
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
                Console.WriteLine(cookie.Name + ": " + cookie.Value);

            Console.ReadLine();
        }

    }

}
