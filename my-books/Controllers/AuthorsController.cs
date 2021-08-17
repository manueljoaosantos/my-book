using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.ViewModel;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.Task<AuthorVM>(author);
            return Ok();
            //return NoContent();
        }

        [HttpGet("get-author-with.books-by-id/{id}")]
        public IActionResult GetAuthorWitBooks(int id)
        {
            var _response = _authorsService.GetAuthorWithBooks(id);
            return Ok(_response);
        }
    }
}
