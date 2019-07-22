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
        public IDocumentStore documentStore { get; set; }

        public IDocumentSession Session
        {
            get
            {
                if (session == null)
                {
                    this.session = this.documentStore.OpenSession();
                }

                return this.session;
            }
        }

        private IDocumentSession session = null;

        public RavenStore()
        {
            this.CreateDocumentStore();
        }

        private void CreateDocumentStore()
        {
            this.documentStore = new DocumentStore
            {
                Urls = new string[] { "http://127.0.0.1:8080" },
                Database = "TestDb",
                Conventions =
                {

                }
            };
            this.documentStore.Initialize();
            this.EnsureDatabaseExists(this.documentStore, "TestDb");
        }

        private void EnsureDatabaseExists(IDocumentStore store, string databaseName)
        {
            if (databaseName.IsNullOrEmpty())
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(databaseName));

            try
            {
                store.Maintenance.ForDatabase(databaseName).Send(new GetStatisticsOperation());
            }
            catch (DatabaseDoesNotExistException)
            {
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
            }
        }
    }
}
