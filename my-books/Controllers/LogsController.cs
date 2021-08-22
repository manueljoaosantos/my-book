using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;
        private readonly ILogger<LogsController> _logger;
        public LogsController(LogsService logsService, ILogger<LogsController> logger)
        {
            _logsService = logsService;
            _logger = logger;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDM()
        {
            try
            {
                var allLogs = _logsService.GetAllLogsFromDB();
                return Ok(allLogs);
            }
            catch (Exception)
            {

                return BadRequest("Não esta a conseguir retornar os logs da base de dados");
            }

        }
    }
}
