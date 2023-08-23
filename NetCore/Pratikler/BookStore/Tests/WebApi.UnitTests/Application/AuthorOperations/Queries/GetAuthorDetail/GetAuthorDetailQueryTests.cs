using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 0;
            //act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenGivenAuthorIdIsInDb_Author_ShouldBeGetBookDetail()
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 1;
            //act
            FluentActions.Invoking(() => query.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == query.AuthorId);
            author.Should().NotBeNull();

            _mapper.Map<AuthorDetailViewModel>(author).Name.Should().Be(author.Name);
        }
    }
}