using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Example.Api.Database;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Example.Api.Controllers
{
    public class ValuesController : BaseController
    {
        private readonly IRavenStore ravenStore;

        public ValuesController(IRavenStore ravenStore)
        {
            this.ravenStore = ravenStore;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var test = new TestClass
            {
                TestString = "String",
                TestInt = 1,
            };
            this.ravenStore.Session.Store(test);
            this.ravenStore.Session.SaveChanges();
            return Ok(new string[] { "value1", "value2" });
        }

        public class TestClass
        {
            public string TestString { get; set; }
            public int TestInt { get; set; }
        }
    }
}
