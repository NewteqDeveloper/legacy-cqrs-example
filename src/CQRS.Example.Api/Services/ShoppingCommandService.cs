using CQRS.Example.Api.Database;
using CQRS.Example.Api.Domain;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public class ShoppingCommandService : IShoppingCommandService
    {
        private readonly IRavenStore ravenStore;
        private readonly INotificationService notificationService;

        public ShoppingCommandService(IRavenStore ravenStore, INotificationService notificationService)
        {
            this.ravenStore = ravenStore;
            this.notificationService = notificationService;
        }

        public async Task AddNewCustomer(Customer customer)
        {
            using (var session = this.ravenStore.DocumentStore.OpenAsyncSession())
            {
                var query = await session.Query<Customer>().ToListAsync();
                var existingCustomer = query.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (existingCustomer == null)
                {
                    await session.StoreAsync(customer);
                    await session.SaveChangesAsync();
                    this.notificationService.MarkAsStale();
                }
            }
        }
    }
}
