using System;
using System.Linq;
using bookApi.Models;

namespace bookApi.DataStores
{
    public interface IDataStore
    {
        IQueryable<Book> GetAll();
        Book GetById(Guid targetId);
    }
}