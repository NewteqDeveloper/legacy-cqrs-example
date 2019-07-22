using CQRS.Example.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Controllers
{
    public class SetupController : BaseController
    {
        private readonly ITestDataService service;

        public SetupController(ITestDataService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Run()
        {
            this.service.CreateTestData();
            return Ok();
        }
    }
}
