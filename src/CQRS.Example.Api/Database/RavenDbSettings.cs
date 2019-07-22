using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Database
{
    public class RavenDbSettings
    {
        public string[] Urls { get; set; }
        public string DatabaseName { get; set; }
    }
}
