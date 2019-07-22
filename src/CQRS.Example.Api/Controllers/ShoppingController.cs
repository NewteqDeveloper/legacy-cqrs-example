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
        private readonly IShoppingService service;

        public ShoppingController(IShoppingService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("customer/all")]
        public IActionResult AllCustomers()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var customers = this.service.GetAllCustomers();
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
            await this.service.AddNewCustomer(customer);
            return Accepted();
        }

        [HttpPost]
        [Route("customer/async/new")]
        public IActionResult AddNewCustomer2([FromBody] Customer customer)
        {
            this.service.AddNewCustomer(customer);
            return Accepted();
        }
    }
}
