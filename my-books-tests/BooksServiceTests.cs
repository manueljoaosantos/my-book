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
    public class BooksServiceTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BooksBbTest")
            .Options;
        AppDbContext context;
        BooksService booksService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            booksService = new BooksService(context);
        }

            
        private void SeedDatabase()
        {

            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publicador 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publicador 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publicador 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publicador 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publicador 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publicador 6"
                    },
                    new Publisher() {
                        Id = 7,
                        Name = "Publicador 7"
                    }
            };
            context.Publishers.AddRange(publishers);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Livro com titulo 1",
                    Description = "Livro 1 com descrição",
                    IsRead = false,
                    Genre = "Romance",
                    CoverUrl = "http://teste.com",
                    DateAdded = DateTime.Now.AddDays(-20),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Livro com titulo 2",
                    Description = "Livro 2 com descrição",
                    IsRead = false,
                    Genre = "Não sei",
                    CoverUrl = "http://teste.com",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 3,
                    Title = "Livro com titulo 3",
                    Description = "Livro 3 com descrição",
                    IsRead = false,
                    Genre = "Guerra",
                    CoverUrl = "http://teste.com",
                    DateAdded = DateTime.Now.AddDays(-50),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void ClearUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}