﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NCommon.Web
{
    class WebReq
    {
        #region 웹서버에 데이터 요청
        /// <summary>
        /// 웹서버에 데이터 요청
        /// </summary>
        /// <param name="strUrl">API URL</param>
        /// <param name="strMethod">POST/GET</param>
        /// <param name="strPost">POST방식일 경우 값</param>
        /// <returns></returns>
        public ApiResult GetWebRequest(string strUrl, string strMethod, string strPost, Encoding encoding, Dictionary<string, string> dicHeader, CookieContainer cookie, int timeout = 120000, string referer = "")
        {
            ApiResult apiResponse = new ApiResult();
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            Uri uri = new Uri(strUrl);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = strMethod;
            request.Timeout = timeout;

            // Header
            if (dicHeader == null || dicHeader.Count() == 0) {
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Credentials = CredentialCache.DefaultCredentials;

                if (referer != "") {
                    request.Referer = referer;
                }
            }

            // 추가 헤더 정보
            if (dicHeader != null) {
                foreach (KeyValuePair<string, string> k in dicHeader) {
                    request.Headers.Add(k.Key, k.Value);
                }
            }

            // Cookie
            request.CookieContainer = cookie;

            WebResponse response = null;

            try {
                if (strMethod == "POST") {
                    // POST 데이터 생성
                    if (!string.IsNullOrEmpty(strPost)) {
                        byte[] bytePost = encoding.GetBytes(strPost);
                        request.ContentLength = bytePost.Length;
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(bytePost, 0, bytePost.Length);
                        dataStream.Close();
                    }
                }

                // 웹페이지 호출
                response = request.GetResponse();

                // 결과 수신
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);

                apiResponse.Html = reader.ReadToEnd();
                apiResponse.StatusCode = ((System.Net.HttpWebResponse)(response)).StatusCode;
                apiResponse.headers = response.Headers;
                apiResponse.cookieContainer = request.CookieContainer;
                apiResponse.WebRequest = request;

            }
            catch (WebException wex) {
                // 결과 수신(에러)
                StreamReader reader = new StreamReader(wex.Response.GetResponseStream(), encoding);

                apiResponse.Html = reader.ReadToEnd();
                apiResponse.StatusCode = ((System.Net.HttpWebResponse)(wex.Response)).StatusCode;
                apiResponse.headers = wex.Response.Headers;
            }
            finally {
                if (response != null)
                    response.Close();
            }

            return apiResponse;
        }
        #endregion

        #region 웹서버에 데이터 요청2
        /// <summary>
        /// 웹서버에 데이터 요청
        /// </summary>
        /// <param name="strUrl">API URL</param>
        /// <returns></returns>
        public ApiResult GetWebRequest(string strUrl, string strMethod, string strPost, Encoding encoding, Dictionary<string, string> dicHeader)
        {
            return GetWebRequest(strUrl, strMethod, strPost, encoding, dicHeader, null);
        }

        /// <summary>
        /// 웹서버에 데이터 요청
        /// </summary>
        /// <param name="strUrl">API URL</param>
        /// <returns></returns>
        public ApiResult GetWebRequest(string strUrl, string strMethod, string strPost, Encoding encoding)
        {
            return GetWebRequest(strUrl, strMethod, strPost, encoding, null, null);
        }

        /// <summary>
        /// 웹서버에 데이터 요청
        /// </summary>
        /// <param name="strUrl">API URL</param>
        /// <returns></returns>
        public ApiResult GetWebRequest(string strUrl, string strMethod, string strPost)
        {
            return GetWebRequest(strUrl, strMethod, strPost, Encoding.UTF8, null, null);
        }

        /// <summary>
        /// 웹서버에 데이터 요청
        /// </summary>
        /// <param name="strUrl">API URL</param>
        /// <returns></returns>
        public ApiResult GetWebRequest(string strUrl)
        {
            return GetWebRequest(strUrl, "GET", null, Encoding.UTF8, null, null);
        }

        #endregion
    }
}
