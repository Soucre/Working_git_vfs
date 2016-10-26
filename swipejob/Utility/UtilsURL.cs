using System;
using System.Net;
using System.Threading.Tasks;

namespace SwipeJob.Utility
{
    public static class UtilsURL
    {
        public static async Task<bool?> IsImage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                return response.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
