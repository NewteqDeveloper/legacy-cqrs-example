using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Database
{
    public class RavenStore : IRavenStore
    {
        public IDocumentStore DocumentStore { get; set; }

        public RavenStore(IOptions<RavenDbSettings> ravenDbSettings)
        {
            this.CreateDocumentStore(ravenDbSettings.Value);
        }

        // Creating the document store
        // https://ravendb.net/docs/article-page/4.1/csharp/client-api/creating-document-store
        private void CreateDocumentStore(RavenDbSettings settings)
        {
            this.DocumentStore = new DocumentStore
            {
                Urls = settings.Urls,
                Database = settings.DatabaseName,
                Conventions =
                {

                }
            };
            this.DocumentStore.Initialize();
            this.DocumentStore.EnsureDatabaseExists(settings.DatabaseName);
        }
    }
}
