using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetById
{
    public class GetBooksIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BooksViewModel Handle(int ID)
        {
            var book = _dbContext.Books.Where(book => book.ID == ID).Select(x => new BooksViewModel
            {
                Title = x.Title,
                Genre = ((GenreEnum)x.GenreId).ToString(),
                PublishDate = x.PublishDate.Date.ToString("dd/MM/yyy"),
                PageCount = x.PageCount
            }).SingleOrDefault();
            return book;
        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}