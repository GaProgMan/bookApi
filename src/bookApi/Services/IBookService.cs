using System;
using bookApi.Models;

namespace bookApi.Services
{
    public interface IGetBook
    {
        Book GetBook(Guid targetBookId);
    }
}