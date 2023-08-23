using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;
            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 1;
            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}