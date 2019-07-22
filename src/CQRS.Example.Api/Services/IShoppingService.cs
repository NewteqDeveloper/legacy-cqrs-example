using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Example.Api.Domain;

namespace CQRS.Example.Api.Services
{
    public interface IShoppingService
    {
        IList<Customer> GetAllCustomers();
        Task AddNewCustomer(Customer customer);
    }
}