using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Example.Api.Controllers
{
    public class ValuesController : BaseController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }
    }
}
