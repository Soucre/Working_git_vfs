using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;

using DestinationBusiness = Vfs.WebCrawler.Destination.Business;
using DestinationData = Vfs.WebCrawler.Destination.Data;
using DestinationEntities = Vfs.WebCrawler.Destination.Entities;

namespace VfsCustomerInformationServices
{
    public abstract class InformationFeedSessionBase
    {
        private List<Exception> listError;
        private int numberOfitem;
        
        public IList<Exception> ListError
        {
            get { return listError; }
        }

        public int NumberOfitem
        {
            get { return numberOfitem; }
        }

        public InformationFeedSessionBase(int numberOfItem)
        {
            this.numberOfitem = numberOfItem;
            this.Initialize();
        }

        //abstract public void FeedNewsList();
        //abstract public void FeedNewsItem(string listUrl, string shortUrl, int linkId);       

        public bool IsFeedAlready()
        {
            return false;
        }

        //public DateTime ConvertStringToDate(string dateString)
        //{
        //    DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
        //    datefomatProvider.DateSeparator = "/";
        //    datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
        //    datefomatProvider.LongDatePattern = "dd/MM/yyyy";
        //    return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

        //    //return DateTime.Parse(dateString);
        //    //return Convert.ToDateTime(dateString, datefomatProvider);
        //}

        public bool FeedItem(StockNew stockNew)
        {
            bool returnVal = true;
            try
            {
                StockNewService.CreateStockNew( stockNew);
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.InnerException);
                returnVal = false;
            }
            return returnVal;
        }

        public void FeedItem(StockNew stockNew, ref Int64 stockId)
        {           
            try
            {
                StockNewService.CreateStockNew(stockNew);
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.InnerException);
                stockId = 0;
            }
            stockId = stockNew.NewsId;
        }

        public bool FeedItemIntoWebSite(Int16 sourceId)
        {
            bool returnVal = true;            
            StockNewCollection stockNewCollection;
            DestinationEntities.stock_New stockNewDestination = new DestinationEntities.stock_New();
            DestinationEntities.stock_NewsGroup stock_NewsGroupDestination = new DestinationEntities.stock_NewsGroup();
            DestinationEntities.stock_NewsGroup stock_NewsGroupHome = new DestinationEntities.stock_NewsGroup();

            Int32 totalRows = 0;
            int pos, symbolId;            
            Int32 newGroupId;
            newGroupId = 2; //nhom tin cong bo thong tin
            try
            {
                stockNewCollection = StockNewService.GetStockNewListBySource(sourceId, StockNewColumns.NewsId, "ASC", 1, 10, out totalRows);
                foreach (StockNew stockNew in stockNewCollection)
                {
                    stockNewDestination.NewsTitle = stockNew.NewsTitle;
                    stockNewDestination.NewsDescription = stockNew.NewsDescription;
                    stockNewDestination.NewsContent = stockNew.NewsContent;
                    stockNewDestination.NewsDate = DateTime.Now.AddHours(-7);
                    stockNewDestination.IsApproved = true;
                    stockNewDestination.LanguageID = 2;// should be dymamic 
                    stockNewDestination.NewsSource = stockNew.NewsSource;
                    pos = stockNewDestination.NewsTitle.Trim().IndexOf(':');
                    symbolId = 0;

                    if (pos > 0) symbolId = GetStockSymbolId(stockNewDestination.NewsTitle.Substring(0, pos));

                    if (symbolId > 0)
                    {
                        stockNew.ShareSymbol = stockNewDestination.NewsTitle.Substring(0, pos);
                    }

                    stockNewDestination.SymbolID = (symbolId > 0 ? symbolId : new int());
                    DestinationBusiness.stock_NewService.Createstock_New(stockNewDestination);

                    stock_NewsGroupDestination.NewsID = stockNewDestination.NewsID;
                    stock_NewsGroupDestination.NewsGroup = newGroupId;
                    DestinationBusiness.stock_NewsGroupService.Createstock_NewsGroup(stock_NewsGroupDestination);

                    stock_NewsGroupHome.NewsID = stockNewDestination.NewsID;
                    stock_NewsGroupHome.NewsGroup = 5;
                    DestinationBusiness.stock_NewsGroupService.Createstock_NewsGroup(stock_NewsGroupHome);
                    Ultility.Error("stock id: " + stockNew.NewsId.ToString());
                    StoredToApprovedNews(stockNew);
                    StockNewService.DeleteStockNew((int)stockNew.NewsId);
                }
            }
            catch (Exception ex)
            {
                Ultility.Error(ex.InnerException);
                returnVal = false;
            }
            return returnVal;
        }

        private void StoredToApprovedNews(StockNew stockNew)
        {
            ApprovedStockNew approvedStockNew = new ApprovedStockNew();
            approvedStockNew.NewsId = stockNew.NewsId;            
            approvedStockNew.NewsDate = stockNew.NewsDate;
            approvedStockNew.NewsTitle = stockNew.NewsTitle;
            approvedStockNew.NewsDescription = stockNew.NewsDescription;
            approvedStockNew.NewsContent = stockNew.NewsContent;
            approvedStockNew.NewsSource = stockNew.NewsSource;
            approvedStockNew.ShareSymbol = stockNew.ShareSymbol;
            approvedStockNew.UseUrl = stockNew.UseUrl;
            approvedStockNew.NewsUrl = stockNew.NewsUrl;
            approvedStockNew.LanguageID = stockNew.LanguageID;
            approvedStockNew.ImageUrl = stockNew.ImageUrl;
            approvedStockNew.LinkId = stockNew.LinkId;
            approvedStockNew.OriginalUrl = stockNew.OriginalUrl;
            approvedStockNew.ApprovedDate = DateTime.Now;
            ApprovedStockNewService.CreateApprovedStockNew(approvedStockNew);
        }

        private int GetStockSymbolId(string symbol)
        {
            Int32 stockSymbolId = 0;
            DestinationEntities.stock_SymbolCollection stock_symbolCollection = DestinationBusiness.stock_SymbolService.Getstock_SymbolList(DestinationEntities.stock_SymbolColumns.Symbol, "ASC");
            foreach (DestinationEntities.stock_Symbol stockSymbol in stock_symbolCollection)
            {
                if (stockSymbol.Symbol == symbol)
                {
                    stockSymbolId = stockSymbol.SymbolID;
                    break;
                }
            }
            return stockSymbolId;
        }
        private void Initialize()
        {
            listError = new List<Exception>();
            numberOfitem = 0;
        }

        private static void WriteReportToDisk(string contents)
        {
            string fileName = ConfigurationManager.AppSettings["logTextFileName"].ToString();
            FileStream fStream = null;
            if (File.Exists(fileName))
            {
                fStream =File.OpenWrite(fileName);
                //fStream = File.AppendText(fileName);
            }
            else
            {
                fStream = File.OpenWrite(fileName);
            }

            using (TextWriter writer = new StreamWriter(fStream))
            {
                writer.WriteLine(contents);
                writer.Flush();
            }
            fStream.Dispose();
        }
    }
}
