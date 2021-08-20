using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookBbTest")
            .Options;
        AppDbContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers);

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
                }
            };
            context.Authors.AddRange(authors);

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
                    Id = 1,
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

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {

                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {

                    Id = 2,
                    BookId = 2,
                    AuthorId =2
                },
                new Book_Author()
                {

                    Id = 3,
                    BookId = 3,
                    AuthorId = 3
                }
            };
            context.Books_Authors.AddRange(books_authors);

            context.SaveChanges();
        }

    [OneTimeTearDown]
        public void ClearUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}