using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenBookIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 0;

            //act & assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı!");

            //assert (Doğrulama)
        }

        [Fact]
        public void WhenGivenBookIdIsInDb_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Dune", GenreId = 1, AuthorId = 1 };
            command.Model = model;
            command.BookId = 1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book => book.ID == command.BookId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);//(_context.Books.SingleOrDefault(a => a.Title == model.Title)).Title==model.Title?default:model.Title
            book.AuthorId.Should().Be(model.AuthorId);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}