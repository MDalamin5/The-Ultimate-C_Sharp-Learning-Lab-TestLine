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
        public ActionResult<List<Book>> GateAllBooks()
        {
            // var bookData = _db.Books.ToList();
            var bookData = _db.Books.Where(b => b.Price >= 300);
            return Ok(bookData);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Book> GetABook(int id)
        {
           var book =  _db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound("Book Not Found.");
            }
            return Ok(book);
        }


        [HttpPut("{id:int}")]
        public ActionResult<string> UpdateBook(Book newBook, int id)
        {
            var book =  _db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound("Book Not Found.");
            }

            book.Author = newBook.Author;
            book.Description = newBook.Description;
            book.Price = newBook.Price;
            book.Title = newBook.Title;

            _db.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteBook(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id);
            if(book == null)
                return NotFound("This Book is not found");
            _db.Books.Remove(book);
            _db.SaveChanges();
            return true;
        }
    }
}