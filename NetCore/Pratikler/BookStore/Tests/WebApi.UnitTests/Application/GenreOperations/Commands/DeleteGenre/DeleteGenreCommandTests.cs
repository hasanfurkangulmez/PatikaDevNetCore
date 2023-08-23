using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenGenreIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 4;
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }
        [Fact]
        public void WhenGivenGenreIdIsInDb_Genre_ShouldBeDeleted()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 3;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().BeNull();
            //Remove Test Yazılacak...
        }
    }
}