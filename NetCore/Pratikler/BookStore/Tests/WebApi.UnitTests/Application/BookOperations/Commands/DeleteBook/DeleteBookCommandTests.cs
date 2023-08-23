using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenBookIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 4;
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenGivenBookIdIsInDb_Book_ShouldBeDeleted()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 3;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var book = _context.Books.SingleOrDefault(book => book.ID == command.BookId);
            book.Should().BeNull();
            //Remove Test Yazılacak...
        }
    }
}