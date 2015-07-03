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
    public class InformationFeedHnxSession: InformationFeedSessionBase, IInformationFeedSession
    {
        public InformationFeedHnxSession(int numberOfItem) : base(numberOfItem) { }

        public void FeedNewsList()
        {
            Source source = SourceService.GetSource(2);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                // Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                //Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedNewsItem(source.URL + lnk.Link, source.URL, lnk.LinkId);
                //Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedNewsItem(string listUrl, string shortUrl, int linkId)
        {
            FeedHnxDocument parseDocument = new FeedHnxDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            FeedHnxDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    HtmlNode link = null;
                    link = TableCells[i];

                    if (link != null)
                    {
                        parseDocument1 = new FeedHnxDocument(shortUrl + link.Attributes["href"].Value.ToString());

                        Console.WriteLine(shortUrl + link.Attributes["href"].Value.ToString().Remove(0, 1));
                        Console.WriteLine("\n");
                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        stockNew.NewsDate = Ultility.ConvertStringToDate(DateTime.Now.ToString("dd/MM/yyyy"));
                        stockNew.NewsDescription = Ultility.ReplaceAsciiToUniCode(link.InnerHtml.Normalize());
                        stockNew.NewsSource = "HNX";
                        stockNew.NewsTitle = Ultility.ReplaceAsciiToUniCode(link.InnerHtml.Normalize());
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString();
                        stockNew.LinkId = linkId;
                        FeedItem(stockNew);                        
                    }
                }
                FeedItemIntoWebSite(2);
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.Message);
            }
        }

    }

}
