using CQRS.Example.Api.Database;
using CQRS.Example.Api.Domain;
using CQRS.Example.Api.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IRavenStore ravenStore;

        private bool isStaleData { get; set; } = true;

        public ShoppingService(IRavenStore ravenStore)
        {
            this.ravenStore = ravenStore;
        }

        public IList<Customer> GetAllCustomers()
        {
            if (isStaleData)
            {
                using (var session = this.ravenStore.DocumentStore.OpenSession())
                {
                    var customers = session.Query<Customer>().ToList();
                    MemoryDb.Database.Customers = customers;
                }
            }

            return MemoryDb.Database.Customers;
        }

        public async Task AddNewCustomer(Customer customer)
        {
            using (var session = this.ravenStore.DocumentStore.OpenAsyncSession())
            {
                var existingCustomer = await session.LoadAsync<Customer>(customer.CustomerId);
                if (existingCustomer == null)
                {
                    await session.StoreAsync(customer);
                    this.isStaleData = true;
                    await session.SaveChangesAsync();
                }
            }
        }
    }
}
