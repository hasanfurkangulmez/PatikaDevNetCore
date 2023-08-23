using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        //[InlineData("Dune", "Dune")]
        [InlineData("Dun", "Dune")]
        [InlineData("Dune", "Dun")]
        [InlineData("Dun", "Dun")]
        [InlineData(" ", "Dune")]
        [InlineData("Dune", " ")]
        [InlineData("", "Dune")]//???Validator???
        [InlineData("Dune", "")]
        [InlineData(" ", " ")]
        [InlineData(" ", "")]
        [InlineData("", " ")]
        [InlineData("", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.Date
            };
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenDateIsGreaterThanToday_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "Yazar",
                Surname = "Dört",
                DateOfBirth = DateTime.Now.Date.AddDays(0)
            };
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "Yazar",
                Surname = "Dört",
                DateOfBirth = DateTime.Now.Date.AddDays(-1)
            };
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}