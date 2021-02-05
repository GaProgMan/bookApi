using System.Collections.Generic;

namespace bookApi.Models
{
    public class PagedResponse
    {
        /// <summary>
        /// The page number for this response
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// How many records there are per page
        /// </summary>
        public int PerPage { get; set;  }
        
        /// <summary>
        /// The books included in this page of results
        /// </summary>
        public List<Book> Records { get; set; }
        
        /// <summary>
        /// The total number of records in the sub set of data which matches the query
        /// </summary>
        public int TotalNumberOfMatchingRecords { get; set; }
    }
}