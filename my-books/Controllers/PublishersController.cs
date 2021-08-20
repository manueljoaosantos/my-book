using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Models.ViewModel;
using my_books.Data.Services;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublishersService publisersService, ILogger<PublishersController> logger)
        {
            _publishersService = publisersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            //throw new Exception("Este é o log de GetAllPublishers()");
            try
            {
                _logger.LogInformation("Este é o log no método GetAllPublishers()");
                var _result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception)
            {

                return BadRequest("Pedims descupa, mas não consiguimos devover od Publicadores");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var _publisher = _publishersService.AddPubliser(publisher);
                //return Ok();
                //return NoContent();
                return Created(nameof(_publisher), _publisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Nome do Publicador: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisheById(int id)
        {
            //throw new Exception("Esta é uma excepção do Handler do middleware");
            var _response = _publishersService.GetPubliserById(id);
            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
            //return Ok(_response);
        }
        //public CustomActionResult GetPublisheById(int id)
        //{
        //    //throw new Exception("Esta é uma excepção do Handler do middleware");
        //    var _response = _publishersService.GetPubliserById(id);
        //    if (_response != null)
        //    {
        //        var _resposeObj = new CustomActionResultVM()
        //        {
        //            Publisher = _response
        //        };

        //        return new CustomActionResult(_resposeObj);
        //    }
        //    else
        //    {
        //        var _resposeObj = new CustomActionResultVM()
        //        {
        //            Exception = new Exception("Esta excenption vem do método get-publisher-by-id/{id}")
        //        };

        //        return new CustomActionResult(_resposeObj);
        //    }
        //    //return Ok(_response);
        //}

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
