using System;
using System.Collections.Generic;
using System.Linq;
using bookApi.Models;

namespace bookApi.Services
{
    public class HardCodedBookService : IGetBook
    {
        private readonly List<Book> _fakeDatabase;
        public HardCodedBookService()
        {
            _fakeDatabase = new List<Book>
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
                },
                new Book
                {
                    Id = Guid.Parse("C5B6439B-E9A3-42C2-9C76-67722AE8E8E5"),
                    Author = "A Third Author",
                    Name = "Lorem Ipsum: The End of an Era"
                },
                new Book
                {
                    Id = Guid.Parse("A9ACEF52-9C67-4C0F-92BC-5A94455EA4B5"),
                    Author = "Some other Peron",
                    Name = "Dotnet Through the ages"
                },
                new Book
                {
                    Id = Guid.Parse("84327A5A-6BF2-423C-AE19-9B136AF978E7"),
                    Author = "Some other Peron",
                    Name = "Dotnet Through the ages - Part 2: Electric Boogaloo"
                },
                new Book
                {
                    Id = Guid.Parse("946B29C0-8B4A-453F-BFF6-65F44D1E067A"),
                    Author = "Some other Peron",
                    Name = "Dotnet Through the ages - Part 3: Open Source Is The Future"
                }
            };
        }
        public Book GetBook(Guid targetBookId)
        {
            return _fakeDatabase.FirstOrDefault(book => book.Id == targetBookId);
        }
    }
}