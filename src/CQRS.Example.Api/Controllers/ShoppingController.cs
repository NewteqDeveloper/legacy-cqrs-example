using CQRS.Example.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public IActionResult AllCustomers()
        {
            return Ok(this.service.GetAllCustomers());
        }
    }
}
