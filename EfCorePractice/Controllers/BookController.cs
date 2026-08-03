using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfCorePractice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {
        public string CreateBook()
        {
            return "Book Created";
        }

        public string GateAllBooks()
        {
            return "List of books";
        }

        public string GetABook()
        {
            return "Single Book";
        }

        public string UpdateBook()
        {
            return "Book has been Updated.";
        }

        public bool DeleteBook()
        {
            return true;
        }
    }
}