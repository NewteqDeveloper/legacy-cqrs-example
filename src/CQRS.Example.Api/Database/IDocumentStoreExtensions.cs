using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Database
{
    public static class IDocumentStoreExtensions
    {
        // Creating a database
        // https://ravendb.net/docs/article-page/4.1/csharp/client-api/operations/server-wide/create-database
        public static void EnsureDatabaseExists(this IDocumentStore store, string databaseName)
        {
            if (databaseName.IsCompletelyEmpty())
                throw new ArgumentException("Value cannot be null or empty.", nameof(databaseName));

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
