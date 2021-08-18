using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PublishersController(PublishersService publisersService)
        {
            _publishersService = publisersService;
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
        //public Publisher GetPublisheById(int id)
        //{
        //    //throw new Exception("Esta é uma excepção do Handler do middleware");
        //    var _response = _publishersService.GetPubliserById(id);
        //    if (_response != null)
        //    {
        //        return _response;
        //    }
        //    else
        //    {
        //        return null;
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
