using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using bookApi.Controllers;
using bookApi.DataStores;
using bookApi.Models;
using bookApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace bookApi.Tests
{
    public class BookServiceTests
    {
        private IQueryable<Book> SetupDataStore()
        {
            return new List<Book>
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
            }.AsQueryable();
        }

        [Fact]
        public void GetPaged_Calls_Service_Returns_PagedResponse()
        {
            // Arrange
            var pagedRequest = new PagedRequest
            {
                PageNumber = 1,
                PerPage = 5
            };

            var dataStoreMock = new Mock<IDataStore>();
            dataStoreMock.Setup(x => x.GetAll()).Returns(SetupDataStore());

            var hardcodedGetBookService = new BookService(dataStoreMock.Object);

            var controller = new BookController(hardcodedGetBookService);

            // Act
            var response = controller.Get(pagedRequest);

            // Assert
            Assert.NotNull(response);

            var okObjectResult = response as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);

            var pagedResponse = okObjectResult.Value as PagedResponse;
            Assert.NotNull(pagedResponse);
            Assert.NotEmpty(pagedResponse.Records);
            Assert.Equal(pagedRequest.PageNumber, pagedResponse.PageNumber);
            Assert.Equal(pagedRequest.PerPage, pagedResponse.PerPage);
            Assert.True(pagedResponse.TotalNumberOfMatchingRecords > 0);

            var firstBook = pagedResponse.Records.FirstOrDefault();
            Assert.NotNull(firstBook);
            Assert.False(string.IsNullOrWhiteSpace(firstBook.Name));
            Assert.False(string.IsNullOrWhiteSpace(firstBook.Author));
            Assert.NotEqual(Guid.Empty, firstBook.Id);
        }
    }
}