using my_books.Data.Models;
using my_books.Data.Models.ViewModel;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        //Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        //public async Task<Publisher> AddPubliser(PublisherVM publisher)
        public Publisher AddPubliser(PublisherVM publisher)
        {
            if(StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Nome começa por um numero!!",
                    publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            //await _context.SaveChangesAsync();
            return _publisher;
        }

        public Publisher GetPubliserById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisher(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();

            }
            else
            {
                throw new Exception($"O publicador com o id: {id} não existe no sistema!");
            }

        }

        //private bool StringStartWithNumber(string name)
        //{
        //    if (Regex.IsMatch(name, @"^\d")) return true;
        //    return false;
        //}
        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
            
    }
}
