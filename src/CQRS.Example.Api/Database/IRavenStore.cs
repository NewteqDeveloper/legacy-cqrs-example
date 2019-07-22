using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Database
{
    public interface IRavenStore
    {
        IDocumentStore DocumentStore { get; set; }
    }
}
