using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using bookApi.Services;
using bookApi.Models;

namespace bookApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IGetBook _bookGetter;

        public BookController(IGetBook bookGetter)
        {
            _bookGetter = bookGetter;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "A requested book was found and returned", typeof(Book))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "A requested book was not found")]
        public ActionResult Get(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                return new BadRequestResult();
            }
            
            var book = _bookGetter.GetBook(bookId);
            if (book == default)
            {
                return new NotFoundResult();
            }
            
            return new OkObjectResult(book);
        }
    }
}
