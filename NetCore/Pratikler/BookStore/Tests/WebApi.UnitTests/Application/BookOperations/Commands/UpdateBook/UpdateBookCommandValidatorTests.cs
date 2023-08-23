using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "Dune", 1, 1)]
        [InlineData(1, " ", 1, 1)]
        [InlineData(1, "Dune", 0, 1)]
        [InlineData(1, "Dune", 1, 0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };
            command.BookId = bookId;
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "Dune",
                GenreId = 1,
                AuthorId = 1
            };
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeLessOrEqualTo(0);
            //result.Errors.Count.Should().Be(0);
        }
    }
}