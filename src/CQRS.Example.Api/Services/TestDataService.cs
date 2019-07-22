using CQRS.Example.Api.Database;
using CQRS.Example.Api.Domain;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly IRavenStore ravenStore;

        public TestDataService(IRavenStore ravenStore)
        {
            this.ravenStore = ravenStore;
        }

        public void CreateTestData()
        {
            using(var session = this.ravenStore.DocumentStore.OpenSession())
            {
                CreateCustomers(session);

                session.SaveChanges();
            }
        }

        private void CreateCustomers(IDocumentSession session)
        {
            if (session.Query<Customer>().Any())
            {
                return;
            }

            session.Store(new Customer
            {
                CustomerId = "920608",
                Birthdate = new DateTime(1992, 6, 8),
                Email = "john@doe.com",
                FirstName = "John",
                LastName = "Doe",
                SignupDate = new DateTime(2000, 1, 1)
            });

            session.Store(new Customer
            {
                CustomerId = "920408",
                Birthdate = new DateTime(1992, 4, 8),
                Email = "jane@doe.com",
                FirstName = "Jane",
                LastName = "Doe",
                SignupDate = new DateTime(2000, 2, 1)
            });

            session.Store(new Customer
            {
                CustomerId = "920706",
                Birthdate = new DateTime(1992, 7, 6),
                Email = "dane@me.com",
                FirstName = "Dane",
                LastName = "You",
                SignupDate = new DateTime(2000, 1, 1)
            });

            session.Store(new Customer
            {
                CustomerId = "920903",
                Birthdate = new DateTime(1992, 9, 3),
                Email = "dave@david.com",
                FirstName = "David",
                LastName = "Dave",
                SignupDate = new DateTime(2010, 5, 5)
            });

            session.Store(new Customer
            {
                CustomerId = "921123",
                Birthdate = new DateTime(1992, 11, 23),
                Email = "man@human.com",
                FirstName = "Human",
                LastName = "Kind",
                SignupDate = new DateTime(2005, 11, 23)
            });
        }

    }
}
