using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var genre = new Genre() { Name = "Test_WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut.");

        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel() { Name = "Test_WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn" };
            command.Model = model;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}