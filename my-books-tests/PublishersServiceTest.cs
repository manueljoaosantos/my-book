using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookBbTest")
            .Options;
        AppDbContext context;
        PublishersService publishersService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publishersService = new PublishersService(context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_WitNoSortBy_WithNoSearchString_WithNoPageNumber()
        {
            var result = publishersService.GetAllPublishers("", "", null);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.AreEqual(result.Count, 5);
        }

        [Test, Order(2)]
        public void GetAllPublishers_WitNoSortBy_WithNoSearchString_WithPageNumber()
        {
            var result = publishersService.GetAllPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WitNoSortBy_WithSearchString_WithPageNumber()
        {
            var result = publishersService.GetAllPublishers("", "3", null);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publicador 3"));

        }

        [Test, Order(4)]
        public void GetAllPublishers_WitSortBy_WithNoSearchString_WithPageNumber()
        {
            var result = publishersService.GetAllPublishers("name_desc", "", null);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publicador 7"));

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
                    Description = "Livro 1 com descri��o",
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
                    Description = "Livro 2 com descri��o",
                    IsRead = false,
                    Genre = "N�o sei",
                    CoverUrl = "http://teste.com",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 3,
                    Title = "Livro com titulo 3",
                    Description = "Livro 3 com descri��o",
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