using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsCustomerInformationServices
{
    public class InformationFeedStoxSession : InformationFeedSessionBase, IInformationFeedSession
    {
        public InformationFeedStoxSession(int numberOfItem) : base(numberOfItem) { }

        public void FeedNewsList()
        {
            Source source = SourceService.GetSource(6);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedNewsItem(source.URL + lnk.Link, source.URL, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedNewsItem(string listUrl, string shortUrl, int linkId)
        {
            FeedStoxDocument parseDocument = new FeedStoxDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            FeedStoxDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);

                    if (link != null)
                    {
                        parseDocument1 = new FeedStoxDocument(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = DateTime.Now;//Ultility.ConvertStringToDate(newDate.InnerHtml.Replace("\r", ""));
                        stockNew.NewsDescription = link.InnerHtml.Normalize();
                        stockNew.NewsSource = "Stox";
                        stockNew.NewsTitle = link.InnerHtml.Normalize();
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1);
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);
                    }
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }
    }
}
