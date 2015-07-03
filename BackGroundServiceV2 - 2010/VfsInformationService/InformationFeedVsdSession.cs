using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;
using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

namespace VfsCustomerInformationServices
{
    public class InformationFeedVsdSession : InformationFeedSessionBase, IInformationFeedSession
    {
        public InformationFeedVsdSession(int numberOfItem) : base(numberOfItem) { }

        public void FeedNewsList()
        {
            Source source = SourceService.GetSource(3);
            LinkCollection links = LinkService.GetLinkListBySourceId(source.SourceId, LinkColumns.LinkId, "ASC");
            foreach (Link lnk in links)
            {
                Ultility.Info("----- Feed from:" + lnk.LinkDescription + " -----");
                Ultility.Info(source.URL + lnk.Link + " , " + source.URL + lnk.LinkShortDescription);
                FeedNewsItem(source.URL + lnk.Link, source.URL + lnk.LinkShortDescription, lnk.LinkId);
                Ultility.Info("----- End Feeding from:" + lnk.LinkDescription + " -----");
            }
        }

        public void FeedNewsItem(string listUrl, string shortUrl, int linkId)
        {
            //testing listUrl = @"http://www.hsx.vn/hsx/Modules/News/News.aspx?type=TTGD";
            FeedVsdDocument parseDocument = new FeedVsdDocument(listUrl);
            List<HtmlNode> TableCells = parseDocument.ParseTableCell();
            HtmlNode spans;
            HtmlNode link;
            FeedVsdDocument parseDocument1;
            StockNew stockNew = null;

            try
            {
                for (int i = 0; i < TableCells.Count; i++)
                {
                    //spans = parseDocument.ParseSpan((HtmlNode)TableCells[i]);
                    link = parseDocument.ParseLink((HtmlNode)TableCells[i]);
                    //parseDocument1 = new ParseDocument("http://www.hsx.vn/hsx/Modules/News/" + link.Attributes["href"].Value.ToString());
                    if (link != null)
                    {
                        parseDocument1 = new FeedVsdDocument(shortUrl + link.Attributes["href"].Value.ToString());

                        HtmlNode content = parseDocument1.ParseContent();
                        stockNew = new StockNew();
                        stockNew.FeedDate = DateTime.Now;
                        stockNew.LanguageID = 2;
                        stockNew.NewsContent = content.InnerHtml;
                        string newDate = link.InnerHtml.Substring(link.InnerHtml.IndexOf('[') + 1, link.InnerHtml.IndexOf(']') - link.InnerHtml.IndexOf('['));
                        stockNew.NewsDate = Ultility.ConvertStringToDate(newDate);
                        stockNew.NewsDescription = link.InnerHtml.Substring(0, link.InnerHtml.IndexOf('['));
                        stockNew.NewsDescription = stockNew.NewsDescription.TrimStart().TrimEnd().Replace("&nbsp", " ");
                        stockNew.NewsSource = "TTLK";
                        /*if (link.InnerHtml.ToString().IndexOf("<img") >= 0)
                            stockNew.NewsTitle = link.InnerHtml.ToString().Remove(link.InnerHtml.ToString().IndexOf("<img"));
                        else
                            stockNew.NewsTitle = link.InnerHtml;*/
                        stockNew.NewsTitle = stockNew.NewsDescription.Replace("&nbsp", " ");
                        stockNew.OriginalUrl = shortUrl + link.Attributes["href"].Value.ToString();
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
