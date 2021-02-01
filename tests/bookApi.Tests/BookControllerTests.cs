using System;
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
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetBook()).Returns(default(Book));

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get();

            // Assert
            Assert.NotNull(response);

            var asNotFound = response as NotFoundResult;
            Assert.NotNull(asNotFound);
        }

        [Fact]
        public void Get_Returns_OkObjectResult_When_RequestedBookCanBeFound()
        {
            // Arrange
            const string targetAuthor = "A.N Author";
            var mockedIGetBook = new Mock<IGetBook>();
            mockedIGetBook.Setup(x => x.GetBook()).Returns(new Book
            {
                Author = targetAuthor
            });

            var controller = new BookController(mockedIGetBook.Object);

            // Act
            var response = controller.Get();

            // Assert
            Assert.NotNull(response);

            var asOkObjectResult = response as OkObjectResult;
            Assert.NotNull(asOkObjectResult);

            var value = asOkObjectResult.Value;
            Assert.NotNull(value);

            var asBook = value as Book;
            Assert.NotNull(asBook);
            Assert.Equal(targetAuthor, asBook.Author);
        }
    }
}