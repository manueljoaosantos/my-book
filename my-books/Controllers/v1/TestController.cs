using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [ApiVersion("1.0")]
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("Este é o teste contoller v1");
        }
    }
}
