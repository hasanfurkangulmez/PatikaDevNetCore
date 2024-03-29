using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        /*private static List<Book> BookList = new List<Book>(){
            new Book{ID=1,
            Title="Lean Startup",
            GenreId=1,//Personal Growth
            PageCount=200,
            PublishDate=new DateTime(2001,06,12)},
            new Book{ID=2,
            Title="Herland",
            GenreId=2,//Science Fiction
            PageCount=250,
            PublishDate=new DateTime(2010,05,23)},
            new Book{ID=3,
            Title="Dune",
            GenreId=2,//Science Fiction
            PageCount=250,
            PublishDate=new DateTime(2001,12,21)}
        };*/

        [HttpGet]
        public IActionResult/*List<Book>*/ GetBooks()
        {
            /*var bookList = BookList.OrderBy(x => x.ID).ToList<Book>();
            return bookList;*/
            /*var bookList = _context.Books.OrderBy(x => x.ID).ToList<Book>();
            return bookList;*/
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public /*Book*/IActionResult GetById(int id)
        {
            /*var book = BookList.Where(book => book.ID == id).SingleOrDefault();
            return book;*/
            /*var book = _context.Books.Where(book => book.ID == id).SingleOrDefault();
            return book;*/
            GetBooksIdQuery query = new GetBooksIdQuery(_context);
            var result = query.Handle(id);
            if (result is null)
                return BadRequest(new InvalidOperationException("Kitap mevcut değil").Message);
            else
                return Ok(result);
        }

        /*[HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.ID == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }*/

        //Post
        [HttpPost]
        public IActionResult AddBook(/*[FromBody] Book newBook*/[FromBody] CreateBookModel newBook)
        {
            /*var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)//book!=null
                return BadRequest();
            BookList.Add(newBook);
            return Ok();*/
            /*var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is not null)//book!=null
                return BadRequest();
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();*/
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //Put
        [HttpPut(("{id}"))]
        public IActionResult UpdateBook(int id, /*[FromBody] Book updatedBook*/ [FromBody] UpdateBookModel updatedBook)
        {
            /*var book = BookList.SingleOrDefault(x => x.ID == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            return Ok();*/
            /*var book = _context.Books.SingleOrDefault(x => x.ID == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();*/

            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            /*var book = BookList.SingleOrDefault(x => x.ID == id);
            if (book is null)
                return BadRequest();

            BookList.Remove(book);
            return Ok();*/
            var book = _context.Books.SingleOrDefault(x => x.ID == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}