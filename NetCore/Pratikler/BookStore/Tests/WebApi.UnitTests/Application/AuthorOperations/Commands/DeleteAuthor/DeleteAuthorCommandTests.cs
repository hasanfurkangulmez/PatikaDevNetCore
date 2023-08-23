using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenAuthorIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 0;
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenGivenAuthorIdIsInDb_Author_ShouldBeDeleted()
        {
            //arrange
            _context.Authors.Add(new Author() { Name = "Yazar", Surname = "Üç", DateOfBirth = new DateTime(1990, 10, 02) });
            _context.SaveChanges();
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 3;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().BeNull();
        }

        [Fact]
        public void WhenGivenAuthorHasBookInPublication_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            _context.Authors.Add(new Author() { Name = "Yazar", Surname = "Üç", DateOfBirth = new DateTime(1990, 10, 02) });
            _context.SaveChanges();
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitabı yayında olan yazar silinemez.");
        }

        [Fact]
        public void WhenGivenAuthorHasNotBookInPublication_Author_ShouldBeDeleted()
        {
            //arrange
            _context.Authors.Add(new Author() { Name = "Yazar", Surname = "Üç", DateOfBirth = new DateTime(1990, 10, 02) });
            _context.SaveChanges();
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 3;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().BeNull();
        }
    }
}