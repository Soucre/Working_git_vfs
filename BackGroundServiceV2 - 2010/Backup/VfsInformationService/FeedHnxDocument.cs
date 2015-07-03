using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace VfsCustomerInformationServices
{
    public class FeedHnxDocument: BaseFeed, IFeedBehaviour
    {
        public FeedHnxDocument() : base() { }

        public FeedHnxDocument(string url) : base(url) { }

        public List<HtmlNode> ParseTableCell()
        {
            //HtmlNode DivNodeIn = htmlDocument.DocumentNode.SelectNodes("//a[@class='textlink1']");                     
            List<HtmlNode> tableCellCollection = new List<HtmlNode>();
            HtmlNodeCollection nodeCollection = HtmlDocument.DocumentNode.SelectNodes("//a[@class='textlink1']");

            foreach (HtmlNode htmlNode in nodeCollection)
            {
                tableCellCollection.Add(htmlNode);
            }

            return tableCellCollection;
        }

        public List<HtmlNode> ParseSpan(HtmlNode node)
        {
            HtmlNode htmlSpan;
            htmlSpan = node.SelectSingleNode("table//span[@class]");
            List<HtmlNode>  nodeCollection = new List<HtmlNode>();
            nodeCollection.Add(htmlSpan);
            return nodeCollection;
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
            HtmlNodeCollection tableCollection = HtmlDocument.DocumentNode.SelectNodes("//table[@width='100%']");
            HtmlNode tableNode = tableCollection[9];
            HtmlNode trNode = tableNode.SelectSingleNode("//tr");
            HtmlNode innerTableNode = trNode.SelectSingleNode("//table");

            HtmlNodeCollection nodeCollection = innerTableNode.SelectNodes("//div[@align='justify']");
            HtmlNodeCollection spanCollection = innerTableNode.SelectNodes("//span[@class='OAnc']");
            HtmlNode divNode = (HtmlNode)nodeCollection[1];

            if (spanCollection != null)
            {
                if (spanCollection.Count > 0)
                {
                    foreach (HtmlNode node in spanCollection)
                    {
                        divNode.InnerHtml = divNode.InnerHtml + "<br />" + "<span>" + node.InnerHtml + "</span>";
                    }
                }
            }
            return divNode;
        }

        public HtmlNode ParseNewsDescription() { return null; }
        public HtmlNode ParseNewsDate(HtmlNode node) { return null; }
    }
}
