namespace SwipeJob.Model.ApiRequset
{
    public class SearchEmployerParams
    {
        public string CompanyName { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}