using System.Collections.Generic;
using CQRS.Example.Api.Domain;

namespace CQRS.Example.Api.Services
{
    public interface IShoppingService
    {
        IList<Customer> GetAllCustomers();
    }
}