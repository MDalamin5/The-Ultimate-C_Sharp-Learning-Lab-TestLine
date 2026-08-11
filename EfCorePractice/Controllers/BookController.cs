using EfCorePractice.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EfCorePractice.Dots;
using EfCorePractice.Models;

namespace EfCorePractice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController: ControllerBase
    {   
        private readonly AppDbContext _db;

        public BookController(AppDbContext appDbContext)
        {
            this._db = appDbContext;   
        }
        [HttpPost]
        public string CreateBook(Book bookData)
        {
            _db.Books.Add(bookData);
            _db.SaveChanges();
            return $"Book id {bookData.Id}, Author Name: {bookData.Author} Book is Created.";
        }

        [HttpGet]
        public List<Book> GateAllBooks()
        {
            var bookData = _db.Books.ToList();
            return bookData;
        }

        [HttpGet("{id:int}")]
        public Book GetABook(int id)
        {
           var book =  _db.Books.FirstOrDefault(b => b.Id == id);
            return book;
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