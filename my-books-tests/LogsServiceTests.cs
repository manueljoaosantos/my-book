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
    public class LogsServiceTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LogsBbTest")
            .Options;
        AppDbContext context;
        LogsService logsService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            logsService = new LogsService(context);
        }
            private void SeedDatabase()
            {
                var logs = new List<Log>()
                {
                    new Log()
                    {
                        Id = 1,
                        Level = "Nivel 1",
                        LogLevel = "Log Nivel 1",
                        Message = "Mensagem numero 1",
                        Exception = "Excepção neumero 1",
                        MessageTemplate = "Messagem Template 1",
                        Properties = "Prpriedades 1",
                        TimeStamp = DateTime.Now.AddDays(-50)
                    },
                    new Log()
                    {
                        Id = 2,
                        Level = "Nivel 2",
                        LogLevel = "Log Nivel 2",
                        Message = "Mensagem numero 2",
                        Exception = "Excepção neumero 2",
                        MessageTemplate = "Messagem Template 2",
                        Properties = "Prpriedades 2",
                        TimeStamp = DateTime.Now.AddDays(-40)
                    },
                    new Log()
                    {
                        Id = 3,
                        Level = "Nivel 3",
                        LogLevel = "Log Nivel 3",
                        Message = "Mensagem numero 3",
                        Exception = "Excepção neumero 3",
                        MessageTemplate = "Messagem Template 3",
                        Properties = "Prpriedades 3",
                        TimeStamp = DateTime.Now.AddDays(-30)
                    },
                    new Log()
                    {
                        Id = 4,
                        Level = "Nivel 1",
                        LogLevel = "Log Nivel 1",
                        Message = "Mensagem numero 4",
                        Exception = "Excepção neumero 4",
                        MessageTemplate = "Messagem Template 4",
                        Properties = "Prpriedades 4",
                        TimeStamp = DateTime.Now.AddDays(-20)
                    },
                    new Log()
                    {
                        Id = 5,
                        Level = "Nivel 1",
                        LogLevel = "Log Nivel 1",
                        Message = "Mensagem numero 5",
                        Exception = "Excepção neumero 5",
                        MessageTemplate = "Messagem Template 5",
                        Properties = "Prpriedades 5",
                        TimeStamp = DateTime.Now.AddDays(-10)
                    },
                    new Log()
                    {
                        Id = 6,
                        Level = "Nivel 2",
                        LogLevel = "Log Nivel 2",
                        Message = "Mensagem numero 6",
                        Exception = "Excepção neumero 6",
                        MessageTemplate = "Messagem Template 6",
                        Properties = "Prpriedades 6",
                        TimeStamp = DateTime.Now.AddDays(-5)
                    },
                    new Log()
                    {
                        Id = 7,
                        Level = "Nivel 1",
                        LogLevel = "Log Nivel 1",
                        Message = "Mensagem numero 7",
                        Exception = "Excepção neumero 7",
                        MessageTemplate = "Messagem Template 7",
                        Properties = "Prpriedades 7",
                        TimeStamp = DateTime.Now.AddDays(-1)
                    }
                };
            context.Logs.AddRange(logs);

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void ClearUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}