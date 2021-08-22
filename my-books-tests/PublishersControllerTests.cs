using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_books.Controllers;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Models.ViewModel;
using my_books.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books_tests
{
    public class PublishersControllerTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookControllerTest")
            .Options;

        AppDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_WithSortBySearchPageNr_ReturnOk_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publicador", 1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publicador 7"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(7));
            Assert.That(actionResultData.Count, Is.EqualTo(5));

            IActionResult actionResultSecondPage = publishersController.GetAllPublishers("name_desc", "Publicador", 2);
            Assert.That(actionResultSecondPage, Is.TypeOf<OkObjectResult>());
            var actionResultDataSecondPage = (actionResultSecondPage as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultDataSecondPage.First().Name, Is.EqualTo("Publicador 2"));
            Assert.That(actionResultDataSecondPage.First().Id, Is.EqualTo(2));
            Assert.That(actionResultDataSecondPage.Count, Is.EqualTo(2));
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_ReturnsOk_Test()
        {
            int publisherId = 1;
            IActionResult actionResult = publishersController.GetPublisheById(publisherId);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var publisherData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(publisherData.Id, Is.EqualTo(1));
            Assert.That(publisherData.Name, Is.EqualTo("publicador 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_ReturnsNotFound_Test()
        {
            int publisherId = 99;
            IActionResult actionResult = publishersController.GetPublisheById(publisherId);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "Novo Publicador"
            };
            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequest_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "123 Novo Publicador"
            };
            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [OneTimeTearDown]
        public void ClearUp()
        {
            context.Database.EnsureDeleted();
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
    }
}
