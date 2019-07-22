using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Domain
{
    public class TimedResult<T>
        where T : class
    {
        public T Results { get; set; }

        public long LoadResultsMs { get; set; }
    }
}
