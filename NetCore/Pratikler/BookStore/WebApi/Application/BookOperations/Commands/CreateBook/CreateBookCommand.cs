using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;//AutoMapper
        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper/*//AutoMapper*/)
        {
            _dbContext = dbContext;
            _mapper = mapper;//AutoMapper
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)//book!=null
                throw new InvalidOperationException("Kitap zaten mevcut.");
            book = _mapper.Map<Book>(Model);// Model ile gelen veriyi book objesine convert et CreateMap config faydalandı//new Book();
            /*book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;*///AutoMapper Kapsamında MappingProfile CreateMap'e taşındı.
                                           //Direkt book database'e kaydedebilecek
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}