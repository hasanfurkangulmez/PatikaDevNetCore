using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author
                    {
                        Name = "Yazar",
                        Surname = "Bir",
                        DateOfBirth = new DateTime(1990, 06, 12)
                    },
                    new Author
                    {
                        Name = "Yazar",
                        Surname = "İki",
                        DateOfBirth = new DateTime(1993, 02, 13)
                    }
                );
        }
    }
}