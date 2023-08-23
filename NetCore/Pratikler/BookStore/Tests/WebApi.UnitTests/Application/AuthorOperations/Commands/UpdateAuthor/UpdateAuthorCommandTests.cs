using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenAuthorIdIsNotInDb_InvalidOperationsException_ShouldBeReturn()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 2;
            //act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenGivenAuthorIdIsInDb_Author_ShouldBeUpdated()
        {
            _context.Authors.Add(new Author() { Name = "YeniYazar", Surname = "YeniSoyad", DateOfBirth = new DateTime(1950, 12, 02) });
            _context.SaveChanges();
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel() { Name = "namesss", Surname = "surname", DateOfBirth = new DateTime(1990, 10, 02) };
            command.Model = model;
            command.AuthorId = 3;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}