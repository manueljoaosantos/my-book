using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Models.ViewModel;
using my_books.Data.Services;
using my_books.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class AuthorsServiceTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "AuthorsBbTest")
            .Options;
        AppDbContext context;
        AuthorsService authorsService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            authorsService = new AuthorsService(context);
        }

            
        private void SeedDatabase()
        {
            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Autor 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Autor 2"
                },
                new Author()
                {
                    Id = 3,
                    FullName = "Autor 3"
                },
                 new Author()
                {
                    Id = 4,
                    FullName = "Autor 4"
                },
                new Author()
                {
                    Id = 5,
                    FullName = "Autor 5"
                },
                new Author()
                {
                    Id = 6,
                    FullName = "Autor 6"
                }
            };
            context.Authors.AddRange(authors);

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void ClearUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}