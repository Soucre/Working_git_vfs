using System;
using System.Collections.Generic;
using System.Text;

namespace VfsCustomerInformationServices
{
    public interface IInformationFeedSession
    {
        void FeedNewsList();
        void FeedNewsItem(string listUrl, string shortUrl, int linkId);
    }
}
