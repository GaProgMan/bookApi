using System;
using System.Linq;
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
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The request data was badly formed")]
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

        [HttpGet("GetPaged")]
        [SwaggerResponse((int)HttpStatusCode.OK, "A page of books", typeof(PagedResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "A the requested page could not be found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The request data was badly formed")]
        public ActionResult Get([FromQuery]PagedRequest requestData)
        {
            if (requestData.PageNumber <= 0 || requestData.PerPage <= 0)
            {
                return new BadRequestResult();
            }

            var pagedDataResponse = _bookGetter.GetPageOfBooks(requestData);

            if (!pagedDataResponse.Records.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(pagedDataResponse);
        }
    }
}
