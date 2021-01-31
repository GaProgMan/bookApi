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
        public ActionResult Get()
        {
            var book = _bookGetter.GetBook();
            if (book == default)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(_bookGetter.GetBook());
        }
    }
}
