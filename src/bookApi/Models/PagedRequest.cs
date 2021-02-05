namespace bookApi.Models
{
    public class PagedRequest
    {
        /// <summary>
        /// The page number that the user is requesting
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// The number of records to return in a single page
        /// </summary>
        public int PerPage { get; set; }
        
        /// <summary>
        /// A column name to sort on
        /// </summary>
        /// <remarks>
        /// Not required
        /// </remarks>
        public string SortColumn { get; set; }
        
        /// <summary>
        /// A direction to sort on (requires the <see cref="SortColumn"/> value)
        /// </summary>
        /// <remarks>
        /// Not required, defaults to Desc if not supplied
        /// </remarks>
        public bool SortDirection { get; set; }
        
        /// <summary>
        /// An arbitrary string to search on across fields
        /// </summary>
        public string SearchQuery { get; set; }
        
        /// <summary>
        /// The number of records to skip when processing this request
        /// </summary>
        public int NumberOfRecordsToSkip => (PageNumber - 1) * PerPage;
    }
}