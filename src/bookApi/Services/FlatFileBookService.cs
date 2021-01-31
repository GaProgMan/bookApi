using bookApi.Models;

namespace bookApi.Services
{
    public class FlatFileBookService : IGetBook
    {
        public FlatFileBookService()
        {
            
        }
        public Book GetBook()
        {
            // TODO go and get some data from somewhere
            return new Book
            {
                Name = "Night Watch"
            };
        }
    }
}