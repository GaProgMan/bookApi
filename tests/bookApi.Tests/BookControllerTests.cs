using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using bookApi.Controllers;
using bookApi.Models;
using bookApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace bookApi.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Get_Returns_NotFound_When_RequestedBookCannotBeFound()
        {
            // Arrange
            var targetBookId = Guid.NewGuid();
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetBook(targetBookId)).Returns(default(Book));

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get(targetBookId);

            // Assert
            Assert.NotNull(response);

            var asNotFound = response as NotFoundResult;
            Assert.NotNull(asNotFound);
        }
        
        [Fact]
        public void Get_Returns_BadRequest_When_RequestedIdIsNotAGuid()
        {
            // Arrange
            var targetBookId = Guid.Empty;
            var mockedIGetBook = new Mock<IGetBook>();
            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get(targetBookId);

            // Assert
            Assert.NotNull(response);

            var asBadRequest = response as BadRequestResult;
            Assert.NotNull(asBadRequest);
        }

        [Fact]
        public void Get_Returns_OkObjectResult_When_RequestedBookCanBeFound()
        {
            // Arrange
            var targetBookId = Guid.NewGuid();
            const string targetAuthor = "A.N Author";
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetBook(targetBookId)).Returns(new Book
            {
                Id = targetBookId,
                Author = targetAuthor
            });

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get(targetBookId);

            // Assert
            Assert.NotNull(response);

            var asOkObjectResult = response as OkObjectResult;
            Assert.NotNull(asOkObjectResult);

            var value = asOkObjectResult.Value;
            Assert.NotNull(value);

            var asBook = value as Book;
            Assert.NotNull(asBook);
            Assert.Equal(targetAuthor, asBook.Author);
            Assert.Equal(targetBookId, asBook.Id);
        }

        [Fact]
        public void GetPaged_ReturnsNotFound_When_RequestedPageCannotBeFound()
        {
            // Arrange
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetPageOfBooks(It.IsAny<PagedRequest>()))
                .Returns(new PagedResponse
                {
                    PageNumber = default,
                    PerPage = default,
                    Records = new List<Book>()
                });

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get(new PagedRequest
                {
                    PageNumber = 1,
                    PerPage = 5
                }
            );

            // Assert
            Assert.NotNull(response);

            var asNotFound = response as NotFoundResult;
            Assert.NotNull(asNotFound);
            Assert.Equal((int)HttpStatusCode.NotFound, asNotFound.StatusCode);
        }
        
        [Fact]
        public void GetPaged_ReturnsBadRequest_When_Request_Is_Invalid()
        {
            // Arrange
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetPageOfBooks(It.IsAny<PagedRequest>()))
                .Returns(new PagedResponse
                {
                    PageNumber = default,
                    PerPage = default,
                    Records = new List<Book>()
                });

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get(new PagedRequest
            {
                PageNumber = 1,
                PerPage = -1
            });

            // Assert
            Assert.NotNull(response);

            var badRequestResult = response as BadRequestResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }
        
        [Fact]
        public void GetPaged_ReturnsOkObjectResult_When_RequestedPageIsFound()
        {
            // Arrange
            var pagedRequest = new PagedRequest
            {
                PageNumber = 1,
                PerPage = 5
            };
            var targetBook = new Book
            {
                Author = "Jamie Taylor",
                Name = "Programming in .NET",
                Id = Guid.Parse("7DC01C54-9E94-4EFC-8AD4-E2E6B46BA9EC")
            };
            
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetPageOfBooks(pagedRequest))
                .Returns(new PagedResponse
                {
                    PageNumber = pagedRequest.PageNumber,
                    PerPage = pagedRequest.PerPage,
                    Records = new List<Book>
                    {
                        targetBook
                    }
                });

            var controller = new BookController(mockedIGetBook.Object);

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

            var firstBook = pagedResponse.Records.FirstOrDefault();
            Assert.NotNull(firstBook);
            Assert.Equal(targetBook.Name, firstBook.Name);
            Assert.Equal(targetBook.Author, firstBook.Author);
            Assert.Equal(targetBook.Id, firstBook.Id);
        }
        
        [Fact]
        public void GetPaged_Calls_Service_Returns_PagedResonse()
        {
            // Arrange
            var pagedRequest = new PagedRequest
            {
                PageNumber = 1,
                PerPage = 5
            };

            var hardcodedGetBookService = new HardCodedBookService();

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