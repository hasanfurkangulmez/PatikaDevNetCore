using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? Model.Name : author.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? Model.Surname : author.Surname;
            author.DateOfBirth = Model.DateOfBirth.Date != DateTime.Now.Date && Model.DateOfBirth.Date != default ? Model.DateOfBirth.Date : author.DateOfBirth.Date;
            _dbContext.SaveChanges();
        }
        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}