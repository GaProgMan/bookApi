using System;
using System.Linq;
using bookApi.Models;

namespace bookApi.Services
{
    public class BookService : IGetBook
    {
        private readonly IDataStore _dataStore;
        
        public BookService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        
        public Book GetBook(Guid targetBookId)
        {
            return _dataStore.GetById(targetBookId);
        }

        public PagedResponse GetPageOfBooks(PagedRequest requestData)
        {
            var query = _dataStore.GetAll();
            
            // TODO Add filtering using SearchQuery, SortColumn, and SortDirection on query
            
            var recordsAsPage = query
                .Skip(requestData.NumberOfRecordsToSkip())
                .Take(requestData.PerPage);
            
            return new PagedResponse
            {
                PageNumber = requestData.PageNumber,
                PerPage = requestData.PerPage,
                TotalNumberOfMatchingRecords = query.Count(),
                Records = recordsAsPage.ToList()
            };
        }
    }
}