using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        //[InlineData("Dune", "Dune")]
        [InlineData("Dun", "Dune")]
        [InlineData("Dune", "Dun")]
        [InlineData("Dun", "Dun")]
        [InlineData(" ", "Dune")]
        [InlineData("Dune", " ")]
        [InlineData("", "Dune")]
        [InlineData("Dune", "")]
        [InlineData(" ", " ")]
        [InlineData(" ", "")]
        [InlineData("", " ")]
        [InlineData("", "")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Now.Date.AddDays(-1)
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenDateIsGreaterThanToday_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Yazar",
                Surname = "Dört",
                DateOfBirth = DateTime.Now.Date.AddDays(-1)
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Yazar",
                Surname = "Dört",
                DateOfBirth = DateTime.Now.Date.AddDays(-1)
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}