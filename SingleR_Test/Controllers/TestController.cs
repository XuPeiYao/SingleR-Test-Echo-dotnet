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
    }
}