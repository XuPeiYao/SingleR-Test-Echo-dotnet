using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SingleR_Test.Controllers {
    [Route("api/[controller]")]
    public class TestController : Controller {
        [HttpGet]
        public string Get() {
            return "OK";
        }

        [HttpPost]
        public IActionResult Post1([FromBody] string data) {
            return RedirectPreserveMethod($"{Request.Scheme}://{Request.Host}/api/Test/t");
        }

        [HttpPost("t")]
        public string Post2([FromBody] string data) {

            return "gg";
        }
    }
}