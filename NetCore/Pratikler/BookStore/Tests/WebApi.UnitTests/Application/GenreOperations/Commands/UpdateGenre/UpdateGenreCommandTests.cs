using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture) => _context = testFixture.Context;

        [Fact]
        public void WhenGivenGenreIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            //act & assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");

            //assert (Doğrulama)
        }

        [Fact]
        public void WhenGivenGenreIdIsInDb_Genre_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { Name = "asdf", IsActive = true };
            command.Model = model;
            command.GenreId = 1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.IsActive.Should().Be(model.IsActive);
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenreName_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { Name = "Personal Growth" };
            command.Model = model;
            command.GenreId = 2;

            //act & assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı İsimli Bir Kitap Türü Zaten Mevcut.");

            //assert (Doğrulama)
        }

        [Fact]
        public void WhenGivenNameIsNotSameWithAnotherGenreName_Genre_ShouldBeUpdated()
        {
            //arrange (Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { Name = "WhenGivenNameIsNotSameWithAnotherGenreName_Genre_ShouldBeUpdated" };
            command.Model = model;
            command.GenreId = 2;

            //act & assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert (Doğrulama)
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(model.IsActive);
        }
    }
}