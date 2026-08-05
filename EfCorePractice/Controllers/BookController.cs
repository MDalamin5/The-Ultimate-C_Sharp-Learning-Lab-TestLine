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


        [HttpPut("{id:int}")]
        public string UpdateBook(int id)
        {
            return $"Id: {id} Book Updated Successfully.";
        }

        [HttpDelete("{id:int}")]
        public bool DeleteBook(int id)
        {
            return true;
        }
    }
}