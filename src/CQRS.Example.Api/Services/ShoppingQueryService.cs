using CQRS.Example.Api.Database;
using CQRS.Example.Api.Domain;
using CQRS.Example.Api.Memory;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public class ShoppingQueryService : IShoppingQueryService
    {
        private readonly IRavenStore ravenStore;
        private readonly INotificationService notificationService;

        private bool isStaleData { get; set; } = true;

        public ShoppingQueryService(IRavenStore ravenStore, INotificationService notificationService)
        {
            this.ravenStore = ravenStore;
            this.notificationService = notificationService;

            this.notificationService.MarkAsStaleRequested += () =>
            {
                isStaleData = true;
            };
        }

        public IList<Customer> GetAllCustomers()
        {
            if (isStaleData)
            {
                using (var session = this.ravenStore.DocumentStore.OpenSession())
                {
                    var customers = session.Query<Customer>().ToList();
                    MemoryDb.Database.Customers = customers;
                    this.isStaleData = false;
                }
            }

            return MemoryDb.Database.Customers;
        }
    }
}
