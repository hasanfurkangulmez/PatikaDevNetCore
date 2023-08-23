using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenGenreIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 4;
            //act & assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenGivenGenreIdIsInDb_Genre_ShouldBeGetGenreDetail()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 2;
            //act
            FluentActions.Invoking(() => query.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == query.GenreId);
            genre.Should().NotBeNull();
            _mapper.Map<GenreDetailViewModel>(genre).Name.Should().Be(genre.Name);
        }
    }
}