using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SafeBoard_SecondTask.DirectoryScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeBoard_SecondTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScannerController : ControllerBase
    {
        private ScannerService _service;

        public ScannerController()
        {
            _service = new ScannerService();
        }

        [HttpGet]
        [Route("scan")]
        public string Scan(string directory)
        {
            var task = _service.AddAndRunNewDefaultTaskAsync(directory);
            return task.Guid.ToString();
        }

        [HttpGet]
        [Route("status")]
        public string Status(Guid guid)
        {
            return _service.GetTaskStatus(guid);
        }
    }
}
