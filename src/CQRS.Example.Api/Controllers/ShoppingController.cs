using CQRS.Example.Api.Domain;
using CQRS.Example.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly IShoppingQueryService queryService;
        private readonly IShoppingCommandService commandService;

        public ShoppingController(IShoppingQueryService queryService, IShoppingCommandService commandService)
        {
            this.queryService = queryService;
            this.commandService = commandService;
        }

        [HttpGet]
        [Route("customer/all")]
        public IActionResult AllCustomers()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var customers = this.queryService.GetAllCustomers();
            stopwatch.Stop();

            var result = new TimedResult<IList<Customer>>
            {
                Results = customers,
                LoadResultsMs = stopwatch.ElapsedMilliseconds,
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("customer/await/new")]
        public async Task<IActionResult> AddNewCustomer([FromBody] Customer customer)
        {
            await this.commandService.AddNewCustomer(customer);
            return Accepted();
        }

        [HttpPost]
        [Route("customer/async/new")]
        public IActionResult AddNewCustomer2([FromBody] Customer customer)
        {
            this.commandService.AddNewCustomer(customer);
            return Accepted();
        }
    }
}
