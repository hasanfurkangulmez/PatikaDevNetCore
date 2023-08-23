using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]//Bu testtir.
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            _context.Authors.Add(new Author() { Name = "YeniYazar", Surname = "YeniSoyad", DateOfBirth = new DateTime(1985, 12, 25) });
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() { Name = "YeniYazar" };
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Zaten Mevcut.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel() { Name = "YeniYazar", Surname = "YeniSoyad", DateOfBirth = new DateTime(1985, 12, 25) };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}