using System;
using System.Collections.Generic;
using System.Linq;
using bookApi.Models;

namespace bookApi.Services
{
    public class HardCodedBookService : IGetBook
    {
        private readonly List<Book> fakeDatabase;
        public HardCodedBookService()
        {
            fakeDatabase = new List<Book>
            {
                new Book
                {
                    Id = Guid.Parse("22DDF0CC-D191-4DDB-B6F9-FE41419AAF50"),
                    Author = "A.N Author",
                    Name = "Lorem Ipsum: The Revenge"
                },
                new Book
                {
                    Id = Guid.Parse("0386A37C-6576-417B-B5B4-DA843E9E4232"),
                    Author = "Some Other-Author",
                    Name = "Lorem Ipsum: Returns"
                }
            };
        }
        public Book GetBook(Guid targetBookId)
        {
            return fakeDatabase.FirstOrDefault(book => book.Id == targetBookId);
        }
    }
}