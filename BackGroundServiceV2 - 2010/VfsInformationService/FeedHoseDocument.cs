using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public class FeedHoseDocument : BaseFeed, IFeedBehaviour
    {
        public FeedHoseDocument() : base() { }

        public FeedHoseDocument(string url) : base(url) { }

        public List<HtmlNode> ParseTableCell()
        {
            HtmlNodeCollection tableCells = HtmlDocument.DocumentNode.SelectNodes("//td[@class]");
            List<HtmlNode> tableCellCollection = new List<HtmlNode>();

            //if (tableCells == null)
            //    return tableCellCollection;

            foreach (HtmlNode cell in tableCells)
            {
                if (cell.Attributes["class"].Value == "news_padding")
                {
                    tableCellCollection.Add(cell);
                }
            }
            return tableCellCollection;
        }

        public List<HtmlNode> ParseSpan(HtmlNode node)
        {
            HtmlNode htmlSpan;
            htmlSpan = node.SelectSingleNode("table//span[@class]");
            List<HtmlNode> htmlSpans = new List<HtmlNode>();
            htmlSpans.Add(htmlSpan);
            return htmlSpans;
        }

        public HtmlNode ParseLink(HtmlNode node)
        {
            HtmlNode htmlHref = null;
            //HtmlNodeCollection tableCells = node.SelectNodes("//a[@class]");
            //foreach (HtmlNode cell in tableCells)
            //{
            //    if (cell.Attributes["class"].Value == "title")
            //    {
            //        htmlHref = cell;
            //    }
            //}
            htmlHref = node.SelectSingleNode("table//a[@class]");
            return htmlHref;
        }

        public HtmlNode ParseContent()
        {
            HtmlNode htmlSpan;
            HtmlNode htmlAttachedFiles = null;
            htmlSpan = HtmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblSumary']");
            htmlSpan.InnerHtml = "<span>" + htmlSpan.InnerHtml + HtmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_lblContent']").InnerHtml + "</span>";

            htmlAttachedFiles = HtmlDocument.DocumentNode.SelectSingleNode("//span[@id='ctl00_mainContent_dtlAttachedFile']");
            HtmlNode nodeTable = HtmlDocument.DocumentNode.SelectSingleNode("//table[@id='ctl00_mainContent_dtlAttachedFile']");

            if (nodeTable != null)
            {
                HtmlNodeCollection rows = nodeTable.SelectNodes("tr");
                if (nodeTable.HasChildNodes)
                {
                    if (rows[1] != null)
                    {
                        HtmlNode cell = ((HtmlNode)rows[1]).SelectSingleNode("td");
                        htmlSpan.InnerHtml = htmlSpan.InnerHtml + "<br/>" + cell.InnerHtml.Replace("../..", "http://www.hsx.vn/hsx");
                    }
                }
            }
            return htmlSpan;
        }
        public HtmlNode ParseNewsDate(HtmlNode node) { return null; }
        public HtmlNode ParseNewsDescription() { return null; }
    }
}
