using EfCorePractice.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfCorePractice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {   
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }
        [HttpPost]
        public string CreateBook()
        {
            return "Book Created";
        }

        [HttpGet]
        public string GateAllBooks()
        {
            return "List of books";
        }

        [HttpGet("{id:int}")]
        public string GetABook(int id)
        {
            return $"Id of the book is {id}";
        }


        // public string UpdateBook()
        // {
        //     return "Book has been Updated.";
        // }

        // public bool DeleteBook()
        // {
        //     return true;
        // }
    }
}