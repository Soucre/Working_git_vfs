namespace SwipeJob.Utility  
{
    public class ApiJsonPagingResult : ApiJsonResult
    {
        public int TotalPages { get; set; }

        public int TotalItems { get; set; }
    }
}
