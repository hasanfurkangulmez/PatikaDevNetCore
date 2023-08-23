using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 0;
            //act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı!");
        }

        [Fact]
        public void WhenGivenBookIdIsInDb_Book_ShouldBeGetBookDetail()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 2;
            //act
            FluentActions.Invoking(() => query.Handle()).Invoke();
            //assert
            var book = _context.Books.SingleOrDefault(book=>book.ID==query.BookId);
            book.Should().NotBeNull();
            
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            vm.Title.Should().Be(book.Title);
        }
    }
}